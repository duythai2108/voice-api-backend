using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities.Enums;

namespace VoiceAPI.Entities
{
    [Table(nameof(JobInvitation))]
    public class JobInvitation
    {
        public Guid Id { get; set; }
        
        public Guid CandidateId { get; set; }

        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }

        public JobInvitationStatusEnum Status { get; set; }

        [ForeignKey(nameof(Id))]
        public Job Job { get; set; }

        [ForeignKey(nameof(CandidateId))]
        public Candidate Candidate { get; set; }
    }
}
