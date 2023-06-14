using MeetUp.CommentsService.Application.RequestFeatures;
using MeetUp.CommentsService.Application.DTOs.OutputDto;
using MeetUp.CommentsService.Application.DTOs.InputDto;
using MeetUp.CommentsService.Infrastructure.Contracts;
using MeetUp.CommentsService.Infrastructure.Models;
using MeetUp.CommentsService.Application.Contracts;
using FluentValidation;
using Mapster;

namespace MeetUp.CommentsService.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IValidator<CommentDto> _commentValidator;
        public CommentService(
            IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
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
            throw new NotImplementedException();
        }

        public async Task<PagedList<OutputCommentDto>> GetAllCommentsAsync(
            CommentQueryDto commentQuery,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<OutputCommentDto> GetCommentByIdAsync(
            Guid commentId,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<OutputCommentDto>> GetCommentsByEventIdAsync(
            Guid eventId,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<OutputCommentDto>> GetCommentsByUserIdAsync(
            string userId,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
