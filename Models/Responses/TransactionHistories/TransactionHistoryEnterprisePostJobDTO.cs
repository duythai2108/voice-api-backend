using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities.Enums;
using VoiceAPI.Models.Responses.Jobs;

namespace VoiceAPI.Models.Responses.TransactionHistories
{
    public class TransactionHistoryEnterprisePostJobDTO
    {
        public Guid Id { get; set; }

        public TransactionTypeEnum TransactionType { get; set; }

        public DateTime CreatedTime { get; set; }

        public JobDTO Job { get; set; }
    }
}
