using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoiceAPI.Entities.Enums;

namespace VoiceAPI.Models.Responses.Candidates
{
    public class CandidateDTO
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }
        public GenderEnum? Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string AvatarUrl { get; set; }

        public AccentEnum Accent { get; set; }

        public string PhoneContact { get; set; }
        public string EmailContact { get; set; }
        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string LinkedinUrl { get; set; }

        public WorkingStatusEnum Status { get; set; }

        public List<string> VoiceLinks { get; set; }

        public string Province { get; set; }
    }
}
