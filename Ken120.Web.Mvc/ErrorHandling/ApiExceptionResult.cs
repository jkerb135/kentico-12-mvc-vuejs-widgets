using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Ken120.Web.Mvc.ErrorHandling
{
    public class ApiExceptionResult : IHttpActionResult
    {
        private static readonly MediaTypeHeaderValue mediaType = new MediaTypeHeaderValue("application/json");

        private readonly ApiError errorResponse;
        private readonly JsonMediaTypeFormatter jsonMediaTypeFormatter;
        private readonly HttpRequestMessage requestMessage;
        private readonly HttpStatusCode statusCode;

        public ApiExceptionResult(HttpRequestMessage requestMessage, HttpStatusCode statusCode, ApiError errorResponse, JsonMediaTypeFormatter jsonMediaTypeFormatter)
        {
            this.requestMessage = requestMessage;
            this.statusCode = statusCode;
            this.errorResponse = errorResponse;
            this.jsonMediaTypeFormatter = jsonMediaTypeFormatter;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = requestMessage.CreateResponse(statusCode, errorResponse, jsonMediaTypeFormatter, mediaType);

            return Task.FromResult(response);
        }
    }
}
