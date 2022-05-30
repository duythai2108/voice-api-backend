using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities.Enums;

namespace VoiceAPI.Entities
{
    [Table(nameof(Job))]
    public class Job
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(100), Required]
        public string Name { get; set; }
        
        [MaxLength(500), Required]
        public string Description { get; set; }

        public int? DayDuration { get; set; }
        public int? HourDuration { get; set; }
        public int? MinuteDuration { get; set; }

        public Guid EnterpriseId { get; set; }

        public Guid SubCategoryId { get; set; }

        public JobStatusEnum JobStatus { get; set; }

        public decimal Price { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        [ForeignKey(nameof(EnterpriseId))]
        public Enterprise Enterprise { get; set; }

        [ForeignKey(nameof(SubCategoryId))]
        public SubCategory SubCategory { get; set; }

        public JobInvitation JobInvitation { get; set; }

        public List<Order> Order { get; set; }
        public List<FavouriteJob> FavouriteJob { get; set; }
    }
}
