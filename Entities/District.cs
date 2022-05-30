using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceAPI.Entities
{
    [Table(nameof(District))]
    [Index(nameof(Name), IsUnique = true)]
    public class District
    {
        [Key, MaxLength(5)]
        public string Code { get; set; }

        [MaxLength(200), Required]
        public string Name { get; set; }

        [Required]
        public string ProvinceCode { get; set; }

        [ForeignKey(nameof(ProvinceCode))]
        public Province Province { get; set; }

        public List<Ward> Wards { get; set; }
    }
}
