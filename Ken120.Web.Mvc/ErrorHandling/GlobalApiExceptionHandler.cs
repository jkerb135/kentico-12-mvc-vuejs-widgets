using System;
using System.Net;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace Ken120.Web.Mvc.ErrorHandling
{
    public class GlobalApiExceptionHandler : IExceptionHandler
    {
        private readonly JsonMediaTypeFormatter jsonMediaTypeFormatter;

        public GlobalApiExceptionHandler(JsonMediaTypeFormatter jsonMediaTypeFormatter) =>
            this.jsonMediaTypeFormatter = jsonMediaTypeFormatter;

        public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            var exception = context.Exception;

            var (code, error) = BuildError(exception);

            context.Result = new ApiExceptionResult(context.Request, code, error, jsonMediaTypeFormatter);

            return Task.CompletedTask;
        }

        private (HttpStatusCode code, ApiError error) BuildError(Exception exception)
        {
            var errorId = Guid.NewGuid();

            if (exception is HttpException httpEx)
            {
                return ((HttpStatusCode)httpEx.GetHttpCode(), new ApiError(errorId, "HTTP_EXCEPTION", "Server error"));
            }

            return (HttpStatusCode.InternalServerError, new ApiError(errorId, "APPLICATION_EXCEPTION", "Server error"));
        }
    }
}
