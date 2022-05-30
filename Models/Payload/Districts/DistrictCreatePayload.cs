using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceAPI.Models.Payload.Districts
{
    public class DistrictCreatePayload
    {
        [Key, MaxLength(5)]
        public string Code { get; set; }

        [MaxLength(200), Required]
        public string Name { get; set; }

        [Required]
        public string ProvinceCode { get; set; }
    }
}
