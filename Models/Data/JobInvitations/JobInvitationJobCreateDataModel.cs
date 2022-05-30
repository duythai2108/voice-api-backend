using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Models.Data.Jobs;

namespace VoiceAPI.Models.Data.JobInvitations
{
    public class JobInvitationJobCreateDataModel
    {
        public JobEnterpriseInviteCandidateForWorkingDataModel JobDataModel { get; set; }
        public Guid CandidateId { get; set; }
    }
}
