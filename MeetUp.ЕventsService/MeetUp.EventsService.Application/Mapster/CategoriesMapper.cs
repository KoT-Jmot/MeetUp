using MeetUp.EventsService.Application.DTOs.InputDto.CategoryDto;
using MeetUp.EventsService.Application.DTOs.OutputDto;
using MeetUp.EventsService.Infrastructure.Models;
using Mapster;

namespace MeetUp.EventsService.Application.Mapster
{
    public class CategoriesMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CategoryDto, Category>();
            config.NewConfig<Category, OutputCategoryDto>();
        }
    }
}
