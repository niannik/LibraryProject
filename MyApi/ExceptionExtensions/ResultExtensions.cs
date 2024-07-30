using Common;
using Microsoft.AspNetCore.Mvc;
using MyApi.Controllers.BadRequestBodyModel;

namespace MyApi.ExceptionExtensions
{
    public static class ResultExtensions
    {
        /// <summary>
        /// This method convert Result type to proper HttpResponse object.
        /// if the result is succeeded, return OkResult,
        /// otherwise return BadRequestObjectResult with result.Error
        /// </summary>
        public static ActionResult ToHttpResponse(this Result result)
        {
            var badRequestBody = new BadRequestBody(result.Error);
            return result.IsSucceeded ? new OkResult() : new BadRequestObjectResult(badRequestBody);
        }

        /// <summary>
        /// This method convert Result&lt;TData&gt; type to proper HttpResponse object.
        /// if the result is succeeded, return OkObjectResult with result.Data,
        /// otherwise return BadRequestObjectResult with result.Error
        /// </summary>
        public static ActionResult<TData> ToHttpResponse<TData>(this Result<TData> result)
        {
            var badRequestBody = new BadRequestBody(result.Error);
            return result.IsSucceeded ? new OkObjectResult(result.Data) : new BadRequestObjectResult(badRequestBody);
        }
    }

}
