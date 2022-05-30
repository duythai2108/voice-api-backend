﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceAPI.Models.Responses.Enterprises
{
    public class EnterpriseDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string LogoUrl { get; set; }

        public string Website { get; set; }

        public string PhoneContact { get; set; }

        public string EmailContact { get; set; }
        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string LinkedinUrl { get; set; }

        public string Description { get; set; }

        public string Province { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
    }
}
