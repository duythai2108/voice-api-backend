using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceAPI.Entities
{
    [Table(nameof(Province))]
    [Index(nameof(Name), IsUnique = true)]
    public class Province
    {
        [Key, MaxLength(5)]
        public string Code { get; set; }

        [MaxLength(200), Required]
        public string Name { get; set; }

        public List<District> Districts { get; set; }
    }
}
