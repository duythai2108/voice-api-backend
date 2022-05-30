using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceAPI.Models.Responses.Districts
{
    public class DistrictDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public string ProvinceCode { get; set; }
    }
}
