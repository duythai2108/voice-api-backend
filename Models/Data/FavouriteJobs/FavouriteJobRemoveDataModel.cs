using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceAPI.Models.Data.FavouriteJobs
{
    public class FavouriteJobRemoveDataModel
    {
        public Guid CandidateId { get; set; }

        public Guid JobId { get; set; }
    }
}
