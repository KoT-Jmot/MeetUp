using MeetUp.CommentsService.Application.Hubs;
using Microsoft.AspNetCore.SignalR;
using Moq;

namespace MeetUp.CommentsService.Tests.UnitTests.ServicesTests
{
    public static class HubContextMock
    {
        public static Mock<IHubContext<CommentsHub>> Create()
        {
            var mockClients = new Mock<IHubClients>();

            var mockClientProxy = new Mock<IClientProxy>();
            mockClients.Setup(x => x.Group(It.IsAny<string>())).Returns(mockClientProxy.Object);

            var mockHubContext = new Mock<IHubContext<CommentsHub>>();
            mockHubContext.Setup(x => x.Clients).Returns(mockClients.Object);

            return mockHubContext;
        }
    }
}
