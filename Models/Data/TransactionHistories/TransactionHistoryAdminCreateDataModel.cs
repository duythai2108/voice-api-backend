using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Models.Payload.TransactionHistories;

namespace VoiceAPI.Models.Data.TransactionHistories
{
    public class TransactionHistoryAdminCreateDataModel
    {
        public Guid AdminId { get; set; }
        public TransactionHistoryAdminCreatePayload Payload { get; set; }
    }
}
