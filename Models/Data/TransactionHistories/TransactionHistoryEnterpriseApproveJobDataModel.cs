using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities;

namespace VoiceAPI.Models.Data.TransactionHistories
{
    public class TransactionHistoryEnterpriseApproveJobDataModel
    {
        [Required]
        public Guid OrderId { get; set; }

        [Required]
        public List<Order> AppliedOrders { get; set; }
    }
}
