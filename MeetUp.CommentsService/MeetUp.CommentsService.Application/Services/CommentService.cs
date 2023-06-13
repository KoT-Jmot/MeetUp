using MeetUp.CommentsService.Application.Contracts;
using MeetUp.CommentsService.Application.DTOs.InputDto;
using MeetUp.CommentsService.Application.DTOs.OutputDto;
using MeetUp.CommentsService.Application.RequestFeatures;
using MeetUp.CommentsService.Infrastructure.Contracts;

namespace MeetUp.CommentsService.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepositoryManager _repositoryManager;
        public CommentService(
            IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<Guid> CreateCommentAsync(CommentDto commentDto, CancellationToken cancellationToken)
        {
            var comment = 

            await _repositoryManager.Comments.AddAsync(comment, cancellationToken);
            await _repositoryManager.SaveChangesAsync(cancellationToken);

            return comment.Id;
        }

        public async Task DeleteCommentByIdAsync(Guid commentId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<OutputCommentDto>> GetAllCommentsAsync(CommentQueryDto commentQuery, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<OutputCommentDto> GetCommentByIdAsync(Guid commentId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<OutputCommentDto>> GetCommentsByEventIdAsync(Guid eventId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<OutputCommentDto>> GetCommentsByUserIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
