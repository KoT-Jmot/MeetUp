using MeetUp.CommentsService.Application.RequestFeatures;
using MeetUp.CommentsService.Application.Utils.Excaption;
using MeetUp.CommentsService.Application.DTOs.OutputDto;
using MeetUp.CommentsService.Application.DTOs.InputDto;
using MeetUp.CommentsService.Infrastructure.Contracts;
using MeetUp.CommentsService.Infrastructure.Models;
using MeetUp.CommentsService.Application.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Mapster;
using Grpc.Net.Client;
using MeetUpGrpc;
using MeetUp.CommentsService.Application.Hubs;
using System.Text.Json;
using Microsoft.AspNetCore.SignalR;

namespace MeetUp.CommentsService.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IValidator<CommentDto> _commentValidator;
        private readonly string _grpcUrl;
        private readonly IHubContext<CommentsHub> _chatHubContext;


        public CommentService(
            IRepositoryManager repositoryManager,
            IValidator<CommentDto> commentValidator,
            IConfiguration configuration,
            IHubContext<CommentsHub> chatHubContext)
        {
            _repositoryManager = repositoryManager;
            _commentValidator = commentValidator;
            _grpcUrl = configuration["GrpcEventConnection"]!;
            _chatHubContext = chatHubContext;
        }

        public async Task<Guid> CreateCommentByUserIdAsync(
            string userId,
            CommentDto commentDto,
            CancellationToken cancellationToken)
        {
            await _commentValidator.ValidateAndThrowAsync(commentDto);

            if(! await IsEventExistAsync(commentDto.EventId))
            {
                throw new EntityNotFoundException("Event was not found!");
            }

            var comment = commentDto.Adapt<Comment>();
            comment.UserId = userId;

            await _repositoryManager.Comments.AddAsync(comment, cancellationToken);
            await _repositoryManager.SaveChangesAsync(cancellationToken);

            await _chatHubContext.Clients.Group(commentDto.EventId.ToString()).SendAsync(JsonSerializer.Serialize<Comment>(comment));

            return comment.Id;
        }

        public async Task DeleteCommentByIdAndUserIdAsync(
            string userId,
            Guid commentId,
            CancellationToken cancellationToken)
        {
            var deletingComment = await _repositoryManager.Comments.GetCommentByIdAndUserIdAsync(userId, commentId, trackChanges: false, cancellationToken);

            if (deletingComment is null)
            {
                throw new EntityNotFoundException("Comment was not found!");
            }

            await _repositoryManager.Comments.RemoveAsync(deletingComment, cancellationToken);
            await _repositoryManager.SaveChangesAsync(cancellationToken);
        }

        public async Task<PagedList<OutputCommentDto>> GetAllCommentsAsync(
            CommentQueryDto commentQuery,
            CancellationToken cancellationToken)
        {
            var comments = _repositoryManager.Comments.GetAll();

            if (!string.IsNullOrEmpty(commentQuery.Text))
            {
                comments = comments.Where(p => p.Text!.Contains(commentQuery.Text!));
            }

            if (!string.IsNullOrEmpty(commentQuery.UserId))
            {
                comments = comments.Where(p => p.UserId.Equals(commentQuery.UserId));
            }

            if (!commentQuery.EventId.Equals(null))
            {
                comments = comments.Where(p => p.EventId.Equals(commentQuery.EventId));
            }

            comments = comments.OrderBy(p => p.CreateDate);

            var totalCount = await comments.CountAsync(cancellationToken);

            var pagingComments = await comments
                                        .Skip((commentQuery.PageNumber - 1) * commentQuery.PageSize)
                                        .Take(commentQuery.PageSize)
                                        .ToListAsync(cancellationToken);

            var outputComments = pagingComments.Adapt<IEnumerable<OutputCommentDto>>();
            var commentsWithMetaData = PagedList<OutputCommentDto>.ToPagedList(outputComments, commentQuery.PageNumber, totalCount, commentQuery.PageSize);

            return commentsWithMetaData;
        }

        public async Task<OutputCommentDto> GetCommentByIdAsync(
            Guid commentId,
            CancellationToken cancellationToken)
        {
            var exectlComment = await _repositoryManager.Comments.GetByIdAsync(commentId, trackChanges: false, cancellationToken);

            if (exectlComment is null)
            {
                throw new EntityNotFoundException("Comment was not found!");
            }

            var outputComment = exectlComment.Adapt<OutputCommentDto>();

            return outputComment;
        }

        protected virtual async Task<bool> IsEventExistAsync(Guid eventId)
        {
            var channel = GrpcChannel.ForAddress(_grpcUrl);
            var client = new Greeter.GreeterClient(channel);

            var response = await client.EventExistAsync(
                new EventRequest { EventId = eventId.ToString() });

            return response.Message;
        }
    }
}
