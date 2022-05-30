using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities.Enums;

namespace VoiceAPI.Models.Payload.JobInvitations
{
    public class JobInvitationCandidateReplyInvitationPayload
    {
        public Guid Id { get; set; }

        public JobInvitationStatusEnum Status { get; set; }
    }
}
