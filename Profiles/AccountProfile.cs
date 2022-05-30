using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities;
using VoiceAPI.Models.Payload.Accounts;
using VoiceAPI.Models.Responses.Accounts;
using VoiceAPI.Models.Responses.Auths;

namespace VoiceAPI.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountCreatePayload>().ReverseMap();
            CreateMap<Account, AccountUpdatePayload>().ReverseMap();

            CreateMap<Account, AccountDTO>().ReverseMap();

            CreateMap<Account, JwtTokenDTO>().ReverseMap();
        }
    }
}
