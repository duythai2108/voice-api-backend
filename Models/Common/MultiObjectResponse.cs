using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceAPI.Models.Common
{
    public class MultiObjectResponse<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public List<T> Data { get; set; }
        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }

        public MultiObjectResponse()
        {

        }

        public MultiObjectResponse(List<T> data)
        {
            this.Data = data;
        }
    }

    public class MultiObjectResponse
    {
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public List<object> Data { get; set; }
        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }

        public MultiObjectResponse(List<object> data)
        {
            Data = data;
        }
    }
}
