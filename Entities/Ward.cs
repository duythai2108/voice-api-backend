using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceAPI.Entities
{
    [Table(nameof(Ward))]
    [Index(nameof(Name), IsUnique = true)]
    public class Ward
    {
        [Key, MaxLength(10)]
        public string Code { get; set; }

        [MaxLength(250), Required]
        public string Name { get; set; }

        [Required]
        public string DistrictCode { get; set; }

        [ForeignKey(nameof(DistrictCode))]
        public District District { get; set; }
    }
}
