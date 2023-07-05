using FluentValidation;
using MeetUp.CommentsService.Application.DTOs.InputDto;
using MeetUp.CommentsService.Application.Validation;

namespace MeetUp.CommentsService.Tests.UnitTests.ValidationTests
{
    public class CommentValidatorTests
    {
        private readonly IValidator<CommentDto> _commentValidator;

        public CommentValidatorTests()
        {
            _commentValidator = new CommentValidator();
        }

        [Theory]
        [InlineData("1789b1c3-34a2-4f4a-7bbf-08db683498b1", "Hello", true)]
        [InlineData("1789b1c3-34a2-4f4a-7bbf-08db683498b1", "a", true)]
        [InlineData("1789b1c3-34a2-4f4a-7bbf-08db683498b1", "", false)]
        [InlineData("1789b1c3-34a2-4f4a-7bbf-08db683498b1", null, false)]
        [InlineData(null, "Hello", false)]
        public void CommentDtoValidatorTests(
            Guid eventId,
            string text,
            bool isValid)
        {
            var commentDto = new CommentDto
            {
                EventId = eventId,
                Text = text
            };

            Assert.Equal(isValid, _commentValidator.Validate(commentDto).IsValid);
        }
    }
}
