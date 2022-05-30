using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities.Enums;

namespace VoiceAPI.Models.Data.JobInvitations
{
    public class JobInvitationCandidateReplyInvitationDataModel
    {
        public Guid Id { get; set; }
        public Guid CandidateId { get; set; }

        public JobInvitationStatusEnum Status { get; set; }
    }
}
