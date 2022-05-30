using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceAPI.Entities
{
    [Table(nameof(FavouriteJob))]
    public class FavouriteJob
    {
        [Key]
        public Guid Id { get; set; }

        public Guid CandidateId { get; set; }

        public Guid JobId { get; set; }

        public DateTime CreatedTime { get; set; }

        [ForeignKey(nameof(CandidateId))]
        public Candidate Candidate { get; set; }

        [ForeignKey(nameof(JobId))]
        public Job Job { get; set; }
    }
}
