﻿using AutoMapper;
using MarketingBox.Postback.Service.Domain.Models;
using MarketingBox.Postback.Service.Postgres.Entities;

namespace MarketingBox.Postback.Service.MapperProfiles
{
    public class LogProfile : Profile
    {
        public LogProfile()
        {
            CreateMap<EventReferenceLog, EventReferenceLogEntity>();
            CreateMap<Grpc.Models.FilterLogsRequest, Domain.FilterLogsRequest>();
            CreateMap<EventReferenceLogEntity, Grpc.Models.EventReferenceLog> ();
        }
    }
}
