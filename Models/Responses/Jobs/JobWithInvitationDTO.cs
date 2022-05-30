using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities.Enums;
using VoiceAPI.Models.Responses.JobInvitations;

namespace VoiceAPI.Models.Responses.Jobs
{
    public class JobWithInvitationDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? DayDuration { get; set; }
        public int? HourDuration { get; set; }
        public int? MinuteDuration { get; set; }

        public Guid EnterpriseId { get; set; }

        public Guid SubCategoryId { get; set; }

        public JobStatusEnum JobStatus { get; set; }

        public decimal Price { get; set; }

        public bool IsPublic { get; set; }

        public JobInvitationDTO JobInvitation { get; set; }
    }
}
