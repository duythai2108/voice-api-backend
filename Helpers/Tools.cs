using FuzzySharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace VoiceAPI.Helpers
{
    public class Tools : ITools
    {
        public string GetUserOfRequest(IEnumerable<Claim> claims)
        {
            var username = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return username;
        }

        public string ConvertPhoneNumberGlobal(string phoneNumber)
        {
            return $"+84{phoneNumber.Substring(1)}";
        }

        public Dictionary<string, string> FSearchForDic(Dictionary<string, string> dic, string search)
        {
            var dicResult = new Dictionary<string, string>();
            var listValue = dic.Values.ToList();
            var listResult = Process.ExtractTop(search, listValue, str => str, limit: 20);
            foreach (var result in listResult)
            {
                var keyValue = dic.FirstOrDefault(d => d.Value.ToLower().Equals(result.Value.ToLower()));
                dicResult.Add(keyValue.Key, keyValue.Value);
            }
            return dicResult;
        }
    }
}
