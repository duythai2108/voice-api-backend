using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities.Enums;

namespace VoiceAPI.Entities
{
    [Table(nameof(TransactionHistory))]
    public class TransactionHistory
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public TransactionTypeEnum TransactionType { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public Guid? AdminId { get; set; }

        public Guid? JobId { get; set; }

        public Guid WalletId { get; set; }

        public DateTime CreatedTime { get; set; }

        [ForeignKey(nameof(AdminId))]
        public Admin Admin { get; set; }

        [ForeignKey(nameof(JobId))]
        public Job Job { get; set; }

        [ForeignKey(nameof(WalletId))]
        public Wallet Wallet { get; set; }
    }
}
