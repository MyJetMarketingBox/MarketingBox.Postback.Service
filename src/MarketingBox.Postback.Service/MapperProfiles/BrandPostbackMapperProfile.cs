using AutoMapper;
using MarketingBox.Postback.Service.Domain.Models.Requests;
using MarketingBox.Registration.Service.Grpc.Requests.Registration;

namespace MarketingBox.Postback.Service.MapperProfiles
{
    public class BrandPostbackMapperProfile : Profile
    {
        public BrandPostbackMapperProfile()
        {
            CreateMap<BrandPostbackRequest, RegistrationCreateRequest>();
        }
    }
}