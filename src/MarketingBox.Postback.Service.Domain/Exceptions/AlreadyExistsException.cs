using System;

namespace MarketingBox.Postback.Service.Domain.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        const string ExceptionFormat = "{0}:{1} already exists.";
        public AlreadyExistsException(string entityName, object value) : base(string.Format(ExceptionFormat, entityName, value))
        { }
    }
}
