using Common.Extensions;
using System;

namespace Common.Exceptions
{
    public class CustomException : ITException
    {
        public int StatusCode { get; set; }

        public CustomException(CustomHttpStatusCodes statusCode) : base(statusCode.ToDisplay())
        {
            StatusCode = (int)statusCode;
        }

        public CustomException(string entityType) : base(entityType + CustomHttpStatusCodes.EntityNotFound.ToDisplay())
        {
            StatusCode = (int)CustomHttpStatusCodes.EntityNotFound;
        }
    }
}
