using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities.Enums;
using VoiceAPI.Models.Responses.Jobs;

namespace VoiceAPI.Models.Responses.Orders
{
    public class OrderCandidateApplyJobDTO
    {
        public Guid Id { get; set; }

        public Guid CandidateId { get; set; }

        public OrderStatusEnum Status { get; set; }

        public DateTime CreatedTime { get; set; }

        public JobDTO Job { get; set; }
    }
}
