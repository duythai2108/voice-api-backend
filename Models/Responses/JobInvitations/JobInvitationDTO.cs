using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities.Enums;

namespace VoiceAPI.Models.Responses.JobInvitations
{
    public class JobInvitationDTO
    {
        public Guid Id { get; set; }

        public Guid CandidateId { get; set; }

        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }

        public JobInvitationStatusEnum Status { get; set; }
    }
}
