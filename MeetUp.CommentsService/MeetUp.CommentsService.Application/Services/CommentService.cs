using MeetUp.CommentsService.Application.RequestFeatures;
using MeetUp.CommentsService.Application.Utils.Excaption;
using MeetUp.CommentsService.Application.DTOs.OutputDto;
using MeetUp.CommentsService.Application.DTOs.InputDto;
using MeetUp.CommentsService.Infrastructure.Contracts;
using MeetUp.CommentsService.Infrastructure.Models;
using MeetUp.CommentsService.Application.Contracts;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Mapster;

namespace MeetUp.CommentsService.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IValidator<CommentDto> _commentValidator;
        public CommentService(
            IRepositoryManager repositoryManager,
            IValidator<CommentDto> commentValidator)
        {
            _repositoryManager = repositoryManager;
            _commentValidator = commentValidator;
        }

        public async Task<Guid> CreateCommentByUserIdAsync(
            string userId,
            CommentDto commentDto,
            CancellationToken cancellationToken)
        {
            await _commentValidator.ValidateAndThrowAsync(commentDto);

            //
            // use gRPC
            //

            var comment = commentDto.Adapt<Comment>();
            comment.UserId = userId;

            await _repositoryManager.Comments.AddAsync(comment, cancellationToken);
            await _repositoryManager.SaveChangesAsync(cancellationToken);

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
    }
}
