using FluentValidation;
using MeetUp.CommentsService.Application.DTOs.InputDto;
using MeetUp.CommentsService.Application.Hubs;
using MeetUp.CommentsService.Application.Services;
using MeetUp.CommentsService.Infrastructure.Contracts;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;

namespace MeetUp.CommentsService.Tests.UnitTests.ServicesTests
{
    public class OverridedCommentsService : CommentService
    {
        public OverridedCommentsService(
            IRepositoryManager repositoryManager,
            IValidator<CommentDto> commentValidator,
            IConfiguration configuration,
            IHubContext<CommentsHub> chatHubContext)
            : base(repositoryManager, commentValidator, configuration, chatHubContext)
        {
        }

        protected override Task<bool> IsEventExistAsync(Guid eventId)
        {
            return Task.Run(() => eventId == Guid.Empty);
        }
    }
}
