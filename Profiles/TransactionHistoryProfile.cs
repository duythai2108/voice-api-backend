using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities;
using VoiceAPI.Models.Data.TransactionHistories;
using VoiceAPI.Models.Payload.TransactionHistories;
using VoiceAPI.Models.Responses.TransactionHistories;

namespace VoiceAPI.Profiles
{
    public class TransactionHistoryProfile : Profile
    {
        public TransactionHistoryProfile()
        {
            CreateMap<TransactionHistory, TransactionHistoryAdminDepositBalanceDTO>().ReverseMap();

            CreateMap<TransactionHistory, TransactionHistoryAdminCreatePayload>().ReverseMap();

            CreateMap<TransactionHistory, TransactionHistoryEnterprisePostJobDTO>().ReverseMap();
            CreateMap<TransactionHistory, TransactionHistoryEnterprisePostJobDataModel>().ReverseMap();

            CreateMap<TransactionHistory, TransactionHistoryCandidateApplyJobDTO>().ReverseMap();
            CreateMap<TransactionHistory, TransactionHistoryCandidateApplyJobDataModel>().ReverseMap();

            CreateMap<TransactionHistory, TransactionHistoryDTO>().ReverseMap();

            CreateMap<TransactionHistory, TransactionHistoryOrderFinishDTO>().ReverseMap();

            CreateMap<TransactionHistory, TransactionHistoryEnterpriseInviteCandidateForWorkingDataModel>().ReverseMap();
            CreateMap<TransactionHistory, TransactionHistoryEnterpriseInviteCandidateForWorkingDTO>().ReverseMap();

            CreateMap<TransactionHistory, TransactionHistoryCandidateReplyInvitationDTO>().ReverseMap();
        }
    }
}
