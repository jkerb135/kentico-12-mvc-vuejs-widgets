using System;

namespace Ken120.Web.Mvc.ErrorHandling
{
    public class ApiError
    {
        public Guid Id { get; }
        public string Code { get; }
        public string Message { get; }

        public ApiError(
            Guid id,
            string code,
            string message)
        {
            Id = id;
            Code = code;
            Message = message;
        }
    }
}
