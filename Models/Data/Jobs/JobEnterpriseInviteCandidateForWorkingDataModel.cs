using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceAPI.Models.Data.Jobs
{
    public class JobEnterpriseInviteCandidateForWorkingDataModel
    {
        [MaxLength(100), Required]
        public string Name { get; set; }

        [MaxLength(500), Required]
        public string Description { get; set; }

        public int? DayDuration { get; set; }
        public int? HourDuration { get; set; }
        public int? MinuteDuration { get; set; }

        public Guid EnterpriseId { get; set; }

        public Guid SubCategoryId { get; set; }

        public decimal Price { get; set; }
    }
}
