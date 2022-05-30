using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Models.Payload.Orders;

namespace VoiceAPI.Models.Data.Orders
{
    public class OrderEnterpriseApproveJobDataModel
    {
        public Guid EnterpriseId { get; set; }
        public OrderEnterpriseApproveJobPayload Payload { get; set; }
    }
}
