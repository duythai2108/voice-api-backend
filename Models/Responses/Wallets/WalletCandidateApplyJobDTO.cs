using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Models.Responses.TransactionHistories;

namespace VoiceAPI.Models.Responses.Wallets
{
    public class WalletCandidateApplyJobDTO
    {
        public Guid Id { get; set; }

        public decimal AvailableBalance { get; set; }

        public decimal LockedBalance { get; set; }

        public TransactionHistoryCandidateApplyJobDTO Transaction { get; set; }
    }
}
