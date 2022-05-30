using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities.Enums;

namespace VoiceAPI.Models.Payload.TransactionHistories
{
    public class TransactionHistoryAdminCreatePayload
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public Guid WalletId { get; set; }
    }
}
