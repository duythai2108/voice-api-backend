using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities;
using VoiceAPI.Models.Payload.Provinces;
using VoiceAPI.Models.Responses.Provinces;

namespace VoiceAPI.Profiles
{
    public class ProvinceProfile : Profile
    {
        public ProvinceProfile()
        {
            CreateMap<Province, ProvinceCreatePayload>().ReverseMap();

            CreateMap<Province, ProvinceDTO>().ReverseMap();
        }
    }
}
