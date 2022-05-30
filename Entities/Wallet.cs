using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceAPI.Entities
{
    [Table(nameof(Wallet))]
    public class Wallet
    {
        [Key]
        public Guid Id { get; set; }

        public decimal AvailableBalance { get; set; }

        public decimal LockedBalance { get; set; }

        [ForeignKey(nameof(Id))]
        public Account Account { get; set; }

        public List<TransactionHistory> TransactionHistories { get; set; }
    }
}
