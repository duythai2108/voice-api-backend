using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceAPI.Models.Responses.FavouriteJobs
{
    public class FavouriteJobDTO
    {
        public Guid Id { get; set; }

        public Guid CandidateId { get; set; }
        public Guid JobId { get; set; }

        public DateTime CreatedTime { get; set; }
    }
}
