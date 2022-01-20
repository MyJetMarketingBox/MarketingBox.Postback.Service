using AutoMapper;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Grpc.Models;

namespace MarketingBox.Postback.Service.MapperProfiles
{
    public class ReferenceReguestProfile : Profile
    {
        public ReferenceReguestProfile()
        {
            CreateMap<FullReferenceRequest, Reference>();
        }
    }
}
