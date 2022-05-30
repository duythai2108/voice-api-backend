using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities;
using VoiceAPI.Models.Data.JobInvitations;
using VoiceAPI.Models.Payload.JobInvitations;
using VoiceAPI.Models.Responses.JobInvitations;

namespace VoiceAPI.Profiles
{
    public class JobInvitationProfile : Profile
    {
        public JobInvitationProfile()
        {
            CreateMap<JobInvitation, JobInvitationDTO>().ReverseMap();

            CreateMap<JobInvitation, JobInvitationCreateDataModel>().ReverseMap();

            CreateMap<JobInvitation, JobInvitationCandidateReplyInvitationDataModel>().ReverseMap();

            CreateMap<JobInvitation, JobInvitationWithJobDetailDTO>().ReverseMap();

            CreateMap<JobInvitationCandidateReplyInvitationPayload, JobInvitationCandidateReplyInvitationDataModel>().ReverseMap();
        }
    }
}
