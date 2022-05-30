using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities.Enums;
using VoiceAPI.Models.Responses.Jobs;
using VoiceAPI.Models.Responses.TransactionHistories;

namespace VoiceAPI.Models.Responses.Orders
{
    public class OrderFinishDTO
    {
        public Guid Id { get; set; }

        public Guid CandidateId { get; set; }

        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }

        public OrderStatusEnum Status { get; set; }

        public TransactionHistoryOrderFinishDTO Transaction { get; set; }
    }
}
