using System;
using VoiceAPI.Entities.Enums;

namespace VoiceAPI.Models.Responses.TransactionHistories
{
    public class TransactionHistoryAdminDepositBalanceDTO
    {
        public Guid Id { get; set; }

        public TransactionTypeEnum TransactionType { get; set; }

        public Guid? AdminId { get; set; }

        public Guid WalletId { get; set; }

        public DateTime CreatedTime { get; set; }
    }
}
