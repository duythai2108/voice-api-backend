using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceAPI.Models.Common
{
    public class SingleObjectResponse<TData>
    {
        public TData Data { get; set; }

        public SingleObjectResponse() { }

        public SingleObjectResponse(TData data)
        {
            this.Data = data;
        }
    }

    public class SingleObjectResponse
    {
        public object Data { get; set; }

        public SingleObjectResponse()
        {

        }

        public SingleObjectResponse(object data)
        {
            this.Data = data;
        }
    }
}
