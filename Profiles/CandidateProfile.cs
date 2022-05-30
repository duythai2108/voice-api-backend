using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities;
using VoiceAPI.Models.Data.Candidates;
using VoiceAPI.Models.Payload.Candidates;
using VoiceAPI.Models.Responses.Candidates;

namespace VoiceAPI.Profiles
{
    public class CandidateProfile : Profile
    {
        public CandidateProfile()
        {
            CreateMap<Candidate, CandidateCreatePayload>().ReverseMap();
            CreateMap<Candidate, CandidateUpdatePayload>().ReverseMap();

            CreateMap<Candidate, CandidateUpdateDataModel>().ReverseMap();

            CreateMap<CandidateUpdatePayload, CandidateUpdateDataModel>().ReverseMap();

            CreateMap<Candidate, CandidateDTO>().ReverseMap();
        }
    }
}
