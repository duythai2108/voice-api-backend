using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Models.Payload.Jobs;

namespace VoiceAPI.Models.Payload.JobInvitations
{
    public class JobInvitationJobCreatePayload
    {
        public JobEnterpriseInviteCandidateForWorkingPayload JobPayload { get; set; }
        public Guid CandidateId { get; set; }
    }
}
