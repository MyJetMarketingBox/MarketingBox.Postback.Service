using System;

namespace MarketingBox.Postback.Service.Domain.Exceptions
{
    public class InternalException : Exception
    {
        const string ErrorMessage = "Something went wrong.";

        public InternalException(Exception exception) : base(ErrorMessage, exception)
        {
        }
        public InternalException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}