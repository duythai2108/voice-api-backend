using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Models.Responses.JobInvitations;
using VoiceAPI.Models.Responses.TransactionHistories;

namespace VoiceAPI.Models.Responses.Wallets
{
    public class WalletCandidateReplyInvitationDTO
    {
        public Guid Id { get; set; }

        public decimal AvailableBalance { get; set; }

        public decimal LockedBalance { get; set; }

        public JobInvitationWithJobDetailDTO JobInvitation { get; set; }

        public TransactionHistoryCandidateReplyInvitationDTO Transaction { get; set; }
    }
}
