using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities.Enums;

namespace VoiceAPI.Models.Responses.Orders
{
    public class OrderDTO
    {
        public Guid Id { get; set; }

        public Guid CandidateId { get; set; }

        public OrderStatusEnum Status { get; set; }

        public Guid JobId { get; set; }

        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
    }
}
