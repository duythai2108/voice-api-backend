using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceAPI.Models.Payload.Provinces
{
    public class ProvinceCreatePayload
    {
        [Key, MaxLength(5)]
        public string Code { get; set; }

        [MaxLength(200), Required]
        public string Name { get; set; }
    }
}
