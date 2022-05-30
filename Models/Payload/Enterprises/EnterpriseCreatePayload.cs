using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceAPI.Models.Payload.Enterprises
{
    public class EnterpriseCreatePayload
    {
        [MaxLength(500), Required]
        public string Name { get; set; }
        public string LogoUrl { get; set; }

        public string Website { get; set; }

        [MaxLength(15)]
        public string PhoneContact { get; set; }

        [MaxLength(500)]
        public string EmailContact { get; set; }
        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string LinkedinUrl { get; set; }

        [MaxLength(5000)]
        public string Description { get; set; }

        public string Province { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }
    }
}
