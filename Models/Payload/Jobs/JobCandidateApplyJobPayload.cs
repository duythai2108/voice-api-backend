using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceAPI.Models.Payload.Jobs
{
    public class JobCandidateApplyJobPayload
    {
        public Guid JobId { get; set; }
    }
}
