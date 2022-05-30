using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities.Enums;

namespace VoiceAPI.Models.Responses.TransactionHistories
{
    public class TransactionHistoryDTO
    {
        public Guid Id { get; set; }

        public TransactionTypeEnum TransactionType { get; set; }

        public decimal Amount { get; set; }

        public Guid? AdminId { get; set; }

        public Guid? JobId { get; set; }

        public DateTime CreatedTime { get; set; }
    }
}
