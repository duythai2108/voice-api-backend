using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities;
using VoiceAPI.Models.Data.Enterprises;
using VoiceAPI.Models.Payload.Enterprises;
using VoiceAPI.Models.Responses.Enterprises;

namespace VoiceAPI.Profiles
{
    public class EnterpriseProfile : Profile
    {
        public EnterpriseProfile()
        {
            CreateMap<Enterprise, EnterpriseCreatePayload>().ReverseMap();
            CreateMap<Enterprise, EnterpriseUpdateDataModel>().ReverseMap();

            CreateMap<Enterprise, EnterpriseDTO>().ReverseMap();

            CreateMap<EnterpriseUpdatePayload, EnterpriseUpdateDataModel>().ReverseMap();
        }
    }
}
