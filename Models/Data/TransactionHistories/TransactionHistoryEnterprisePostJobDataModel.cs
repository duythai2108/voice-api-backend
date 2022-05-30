using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceAPI.Models.Data.TransactionHistories
{
    public class TransactionHistoryEnterprisePostJobDataModel
    {
        public Guid WalletId { get; set; }
        public Guid JobId { get; set; }
        public decimal Amount { get; set; }
    }
}
