using MeetUp.EventsService.Application.Contracts;
using MeetUp.EventsService.Application.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace MeetUp.EventsService.Api.Features
{
    public class PagingActionResult<T> : IActionResult where T : IPagination
    {
        private readonly T _result;

        public PagingActionResult(T result)
        {
            _result = result;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var headers = new Dictionary<string, StringValues>
            {
                {ResponseHeaders.CurrentPage, _result.MetaData!.CurrentPage.ToString()},
                {ResponseHeaders.TotalCount, _result.MetaData.TotalCount.ToString()},
                {ResponseHeaders.TotalPages, _result.MetaData.TotalPages.ToString()},
                {ResponseHeaders.PageSize, _result.MetaData.PageSize.ToString()},
                {ResponseHeaders.HasNext, _result.MetaData.HasNext.ToString()},
                {ResponseHeaders.HasPrevious, _result.MetaData.HasPrevious.ToString()}

            };

            foreach (var item in headers)
            {
                context.HttpContext.Response.Headers.Add(item);
            }

            var objResult = new ObjectResult(_result)
            {
                StatusCode = StatusCodes.Status200OK
            };

            await objResult.ExecuteResultAsync(context);
        }
    }
}
