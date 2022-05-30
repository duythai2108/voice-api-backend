using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Models.Payload.Jobs;

namespace VoiceAPI.Models.Data.Jobs
{
    public class JobCandidateApplyJobDataModel
    {
        public Guid CandidateId { get; set; }
        public JobCandidateApplyJobPayload Payload { get; set; }
    }
}
