using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities.Enums;

namespace VoiceAPI.Models.Payload.Accounts
{
    public class AccountCreatePayload
    {
        [MaxLength(15), Phone]
        public string PhoneNumber { get; set; }

        [MaxLength(500), EmailAddress]
        public string Email { get; set; }

        [MaxLength(30)]
        public string Password { get; set; }

        public RoleEnum Role { get; set; }
    }
}
