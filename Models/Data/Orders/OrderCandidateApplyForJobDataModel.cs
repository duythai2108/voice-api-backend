using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities.Enums;
using VoiceAPI.Models.Payload.Orders;

namespace VoiceAPI.Models.Data.Orders
{
    public class OrderCandidateApplyForJobDataModel
    {
        public Guid CandidateId { get; set; }

        public OrderCandidateApplyForJobPayload Payload { get; set; }
    }
}
