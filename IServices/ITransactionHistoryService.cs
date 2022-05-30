using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Models.Common;
using VoiceAPI.Models.Data.TransactionHistories;
using VoiceAPI.Models.Responses.Jobs;
using VoiceAPI.Models.Responses.TransactionHistories;
using VoiceAPI.Models.Responses.Wallets;

namespace VoiceAPI.IServices
{
    public interface ITransactionHistoryService
    {
        Task<GenericResult<WalletAdminDepositBalanceDTO>> AdminDepositBalance(TransactionHistoryAdminCreateDataModel dataModel);
        Task<GenericResult<WalletEnterprisePostJobDTO>> EnterprisePostJob(TransactionHistoryEnterprisePostJobDataModel dataModel);
        Task<GenericResult<WalletCandidateApplyJobDTO>> CandidateApplyJob(TransactionHistoryCandidateApplyJobDataModel dataModel);
        Task<GenericResult<JobDTO>> EnterpriseApproveJob(TransactionHistoryEnterpriseApproveJobDataModel dataModel);
        Task<GenericResult<WalletOrderFinishDTO>> FinishOrder(Guid orderId);
        Task<GenericResult<WalletEnterpriseInviteCandidateForWorkingDTO>> InviteCandidateForWorking(TransactionHistoryEnterpriseInviteCandidateForWorkingDataModel dataModel);
        Task<GenericResult<WalletCandidateReplyInvitationDTO>> AcceptJobInvitation(TransactionHistoryCandidateAcceptInvitationDataModel dataModel);
    }
}
