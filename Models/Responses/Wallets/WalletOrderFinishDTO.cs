using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Models.Responses.Orders;
using VoiceAPI.Models.Responses.TransactionHistories;

namespace VoiceAPI.Models.Responses.Wallets
{
    public class WalletOrderFinishDTO
    {
        public Guid Id { get; set; }

        public decimal AvailableBalance { get; set; }

        public decimal LockedBalance { get; set; }

        public OrderFinishDTO Order { get; set; }
    }
}
