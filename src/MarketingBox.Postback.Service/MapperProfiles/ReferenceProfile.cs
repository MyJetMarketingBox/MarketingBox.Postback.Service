using AutoMapper;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Postgres.Entities;

namespace MarketingBox.Postback.Service.MapperProfiles
{
    public class ReferenceProfile : Profile
    {
        public ReferenceProfile()
        {
            CreateMap<Reference, ReferenceEntity>();
        }
    }
}
