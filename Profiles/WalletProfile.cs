using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities;
using VoiceAPI.Models.Responses.TransactionHistories;
using VoiceAPI.Models.Responses.Wallets;

namespace VoiceAPI.Profiles
{
    public class WalletProfile : Profile
    {
        public WalletProfile()
        {
            CreateMap<Wallet, WalletAdminDepositBalanceDTO>().ReverseMap();

            CreateMap<Wallet, WalletEnterprisePostJobDTO>().ReverseMap();

            CreateMap<Wallet, WalletCandidateApplyJobDTO>().ReverseMap();

            CreateMap<Wallet, WalletWithTransactionsDTO>().ReverseMap();

            CreateMap<Wallet, WalletOrderFinishDTO>().ReverseMap();

            CreateMap<Wallet, WalletEnterpriseInviteCandidateForWorkingDTO>().ReverseMap();

            CreateMap<Wallet, WalletCandidateReplyInvitationDTO>().ReverseMap();
        }
    }
}
