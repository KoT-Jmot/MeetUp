using FluentValidation;
using MeetUp.EventsService.Application.DTOs.InputDto.EventDto;
using MeetUp.EventsService.Application.Validation;

namespace MeetUp.EventsService.Tests.UnitTests.ValidationTests
{
    public class EventValidatorTests
    {
        private readonly IValidator<EventDto> _eventValidator;

        public EventValidatorTests()
        {
            _eventValidator = new EventValidation();
        }

        [Theory]
        [InlineData("second", "TestDescription", "Minsk", "2029-06-24T17:00", "21f85435-204d-4e1f-80bb-08db734a088b", true)]
        [InlineData("", "TestDescription", "Minsk", "2029-06-24T17:00", "21f85435-204d-4e1f-80bb-08db734a088b", false)]
        [InlineData("awa", "TestDescription", "Minsk", "2029-06-24T17:00", "21f85435-204d-4e1f-80bb-08db734a088b", false)]
        [InlineData("iqobiafimasfqpgczseeunfgcvekkrmgfeyunqpeekunvqmlgmq", "TestDescription", "Minsk", "2029-06-24T17:00", "21f85435-204d-4e1f-80bb-08db734a088b", false)] // 51
        [InlineData("iqobiafimasfqpgczseeunfgcvekkrmgfeyunqpeekunvqmlgm", "TestDescription", "Minsk", "2029-06-24T17:00", "21f85435-204d-4e1f-80bb-08db734a088b", true)] // 50
        [InlineData("second", "", "Minsk", "2029-06-24T17:00", "21f85435-204d-4e1f-80bb-08db734a088b", true)]
        [InlineData("second", "TestDescription", "", "2029-06-24T17:00", "21f85435-204d-4e1f-80bb-08db734a088b", false)]
        [InlineData("second", "TestDescription", "awa", "2029-06-24T17:00", "21f85435-204d-4e1f-80bb-08db734a088b", false)]
        [InlineData("second", "TestDescription", "Minsk", "2020-06-24T17:00", "21f85435-204d-4e1f-80bb-08db734a088b", false)]

        public void EventDtoValidatorTests(
            string title,
            string description,
            string place,
            DateTime dateStart,
            Guid categoryId,
            bool isValid)
        {
            var eventDto = new EventDto()
            {
                Title = title,
                Description = description,
                Place = place,
                DateStart = dateStart,
                CategoryId = categoryId
            };

            var a = _eventValidator.Validate(eventDto);

            Assert.Equal(isValid, _eventValidator.Validate(eventDto).IsValid);
        }
    }
}
