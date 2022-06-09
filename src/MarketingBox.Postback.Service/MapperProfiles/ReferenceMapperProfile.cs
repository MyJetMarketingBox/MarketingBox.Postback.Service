using AutoMapper;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Domain.Models.Requests;

namespace MarketingBox.Postback.Service.MapperProfiles
{
    public class ReferenceMapperProfile : Profile
    {
        public ReferenceMapperProfile()
        {
            CreateMap<CreateOrUpdateReferenceRequest, Reference>();
        }
    }
}