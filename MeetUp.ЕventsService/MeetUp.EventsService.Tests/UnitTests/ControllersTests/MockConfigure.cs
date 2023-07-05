using MeetUp.EventsService.Api.Controllers;
using MeetUp.EventsService.Application.Contracts;
using MeetUp.EventsService.Application.DTOs.InputDto.CategoryDto;
using MeetUp.EventsService.Application.DTOs.InputDto.EventDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MeetUp.EventsService.Tests.UnitTests.ControllersTests
{
    public static class MockConfigure
    {
        public static Mock<IEventService> CreateEventService()
        {
            var eventService = new Mock<IEventService>();

            eventService.Setup(r => r.GetAllEventsAsync(
                         It.IsAny<EventQueryDto>(),
                         It.IsAny<CancellationToken>()))
                        .ReturnsAsync(EventDataFactory.GetOutputEventsDto());

            eventService.Setup(r => r.GetEventByIdAsync(
                         It.IsAny<Guid>(),
                         It.IsAny<CancellationToken>()))
                        .ReturnsAsync(EventDataFactory.GetOutputEventDto());

            eventService.Setup(r => r.CreateEventBySponserIdAsync(
                         EventDataFactory.GetEventDto(),
                         EventDataFactory.GetUserId,
                         It.IsAny<CancellationToken>()))
                        .ReturnsAsync(EventDataFactory.GetEventEntity().Id);

            eventService.Setup(r => r.UpdateEventByIdAndSponserIdAsync(
                         EventDataFactory.GetEventEntity().Id,
                         EventDataFactory.GetEventDto(),
                         EventDataFactory.GetUserId,
                         It.IsAny<CancellationToken>()))
                        .ReturnsAsync(EventDataFactory.GetEventEntity().Id);

            return eventService;
        }

        public static Mock<ICategoryService> CreateCategoryServiceMock()
        {
            var categoryService = new Mock<ICategoryService>();

            categoryService.Setup(r => r.GetAllCategoriesAsync(
                            It.IsAny<CategoryQueryDto>(),
                            It.IsAny<CancellationToken>()))
                           .ReturnsAsync(CategoryDataFactory.GetAllOutputCategoriesDto());

            categoryService.Setup(r => r.GetCategoryByIdAsync(
                            CategoryDataFactory.GetCategoryEntity().Id,
                            It.IsAny<CancellationToken>()))
                           .ReturnsAsync(CategoryDataFactory.GetOutputCategoryDto());

            categoryService.Setup(r => r.CreateCategoryAsync(
                            CategoryDataFactory.GetCategoryDto(),
                            It.IsAny<CancellationToken>()))
                           .ReturnsAsync(CategoryDataFactory.GetCategoryEntity().Id);

            categoryService.Setup(r => r.DeleteCategoryByIdAsync(
                            It.IsAny<Guid>(),
                            It.IsAny<CancellationToken>()));

            categoryService.Setup(r => r.UpdateCategoryByIdAsync(
                            It.IsAny<Guid>(),
                            CategoryDataFactory.GetCategoryDto(),
                            It.IsAny<CancellationToken>()))
                           .ReturnsAsync(Guid.NewGuid());

            return categoryService;
        }

        public static void CreateEventControllerRequestMock(this EventsController eventsController)
        {
            var mockRequest = new Mock<HttpRequest>();
            var mockHttpContext = new Mock<HttpContext>();

            mockHttpContext.SetupGet(x => x.Request).Returns(mockRequest.Object);

            mockRequest.Setup(r => r.Headers).Returns(new HeaderDictionary
            {
                {"claims_UserId",EventDataFactory.GetUserId }
            });

            eventsController.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            };
        }
    }
}
