using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities;
using VoiceAPI.Models.Payload.Districts;
using VoiceAPI.Models.Responses.Districts;

namespace VoiceAPI.Profiles
{
    public class DistrictProfile : Profile
    {
        public DistrictProfile()
        {
            CreateMap<District, DistrictCreatePayload>().ReverseMap();

            CreateMap<District, DistrictDTO>().ReverseMap();
        }
    }
}
