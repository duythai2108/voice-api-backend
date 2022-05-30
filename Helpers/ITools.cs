using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace VoiceAPI.Helpers
{
    public interface ITools
    {
        string GetUserOfRequest(IEnumerable<Claim> claims);
        string ConvertPhoneNumberGlobal(string phoneNumber);
        Dictionary<string, string> FSearchForDic(Dictionary<string, string> dic, string search);
    }
}
