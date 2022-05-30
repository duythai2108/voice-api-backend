using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceAPI.Entities
{
    [Table(nameof(SubCategory))]
    [Index(nameof(Name), IsUnique = true)]
    public class SubCategory
    {
        [Key]
        public Guid Id { get; set; }
        
        [MaxLength(100), Required]
        public string Name { get; set; }

        public Guid CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        public List<Job> Jobs { get; set; }
    }
}
