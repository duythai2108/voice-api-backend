using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceAPI.Models.Payload.Orders
{
    public class OrderEnterpriseApproveJobPayload
    {
        public Guid OrderId { get; set; }
        public Guid JobId { get; set; }
    }
}
