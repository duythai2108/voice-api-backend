using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities.Enums;
using VoiceAPI.Models.Responses.Jobs;
using VoiceAPI.Models.Responses.Orders;

namespace VoiceAPI.Models.Responses.TransactionHistories
{
    public class TransactionHistoryCandidateApplyJobDTO
    {
        public Guid Id { get; set; }

        public TransactionTypeEnum TransactionType { get; set; }

        public DateTime CreatedTime { get; set; }

        public OrderCandidateApplyJobDTO Order { get; set; }
    }
}
