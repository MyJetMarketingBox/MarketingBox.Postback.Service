using System;
using MarketingBox.Postback.Service.Domain.Exceptions;
using MarketingBox.Postback.Service.Grpc.Models;

namespace MarketingBox.Postback.Service.Helper
{
    public static class ServiceHelper
    {
        public static Response<T> FailedResponse<T>(this Exception ex)
        {
            var statusCode = ex switch
            {
                NotFoundException _ => StatusCode.NotFound,
                AlreadyExistsException _ => StatusCode.BadRequest,
                _ => StatusCode.InternalError
            };

            return new Response<T>
            {
                StatusCode = statusCode,
                ErrorMessage = ex.Message
            };
        }
    }
}