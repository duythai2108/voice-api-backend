using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities.Enums;

namespace VoiceAPI.Models.Payload.Accounts
{
    public class AccountUpdatePayload
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [MaxLength(500)]
        public string Email { get; set; }

        public AccountStatusEnum Status { get; set; }

        public RoleEnum Role { get; set; }
    }
}
