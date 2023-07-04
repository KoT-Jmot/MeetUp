using FluentValidation;
using MeetUp.EventsService.Application.DTOs.InputDto.CategoryDto;
using MeetUp.EventsService.Application.Validation;

namespace MeetUp.EventsService.Tests.UnitTests.ValidationTests
{
    public class CategoryValidatorTests
    {
        private readonly IValidator<CategoryDto> _categoryValidator;

        public CategoryValidatorTests()
        {
            _categoryValidator = new CategoryValidator();
        }

        [Theory]
        [InlineData("First", true)]
        [InlineData("", false)]
        [InlineData("a", false)]
        [InlineData("ajgorlfidnfuaudjsnauq", false)]
        public void CategoryDtoValidatorTests(
            string name,
            bool isValid)
        {
            var categoryDto = new CategoryDto
            {
                Name = name,
            };

            Assert.Equal(isValid, _categoryValidator.Validate(categoryDto).IsValid);
        }
    }
}
