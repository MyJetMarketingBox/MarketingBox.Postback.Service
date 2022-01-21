using System;

namespace MarketingBox.Postback.Service.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        const string ExceptionFormat = "{0}:{1} was not found.";
        public NotFoundException(string entityName, object value): base(string.Format(ExceptionFormat, entityName, value))
        { }
    }
}
