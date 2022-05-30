using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities.Enums;

namespace VoiceAPI.Models.Responses.Orders
{
    public class OrderCandidateReplyInvitationDTO
    {
        public Guid Id { get; set; }

        public Guid CandidateId { get; set; }

        public OrderStatusEnum Status { get; set; }

        public DateTime CreatedTime { get; set; }
    }
}
