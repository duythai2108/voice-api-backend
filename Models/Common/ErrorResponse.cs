using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceAPI.Models.Common
{
    public class ErrorResponse
    {
        public string ErrorCode { get; set; }
        public string Msg { get; set; }
    }
}
