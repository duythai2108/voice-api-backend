using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities.Enums;

namespace VoiceAPI.Models.Responses.Auths
{
    public class JwtTokenDTO
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public RoleEnum Role { get; set; }
        public AccountStatusEnum Status { get; set; }
        public string JwtToken { get; set; }
    }
}
