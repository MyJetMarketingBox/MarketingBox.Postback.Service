using AutoMapper;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Domain.Models.Requests;
using MarketingBox.Registration.Service.Grpc.Requests.Registration;
using GrpcModel = MarketingBox.Registration.Service.Domain.Models.Registrations;

namespace MarketingBox.Postback.Service.MapperProfiles
{
    public class BrandPostbackMapperProfile : Profile
    {
        public BrandPostbackMapperProfile()
        {
            CreateMap<BrandPostbackRequest, RegistrationCreateRequest>()
                .ForMember(x => x.BrandInfo, x => x.MapFrom(z => z.RegistrationBrandInfo));
            
            CreateMap<GeneralInfo, GrpcModel.RegistrationGeneralInfo>();
            CreateMap<AdditionalInfo, GrpcModel.RegistrationAdditionalInfo>();
            CreateMap<RegistrationBrandInfo, GrpcModel.RegistrationBrandInfo>();
        }
    }
}