using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceAPI.Models.Common
{
    public class GenericResult<TData>
    {
        public int StatusCode { get; set; }
        public string ErrorCode { get; set; }
        public string Msg { get; set; }
        public TData Data { get; set; }
        public bool IsSuccess { get; set; }

        public static GenericResult<TData> Success(TData data)
        {
            return new GenericResult<TData> { Data = data, IsSuccess = true };
        }

        public static GenericResult<TData> Error(string errorCode, string msg)
        {
            return new GenericResult<TData> { ErrorCode = errorCode, Msg = msg, IsSuccess = false };
        }

        public static GenericResult<TData> Error(int statusCode, string msg)
        {
            return new GenericResult<TData>
            {
                StatusCode = statusCode,
                ErrorCode = statusCode.ToString(),
                Msg = msg,
                IsSuccess = false
            };
        }

        public static GenericResult<TData> Error(int statusCode, string errorCode, string msg)
        {
            return new GenericResult<TData>
            {
                StatusCode = statusCode,
                ErrorCode = errorCode,
                Msg = msg,
                IsSuccess = false
            };
        }
    }

    public class GenericResult
    {
        public string ErrorCode { get; set; }
        public string Msg { get; set; }
        public bool IsSuccess { get; set; }
        public static GenericResult Success()
        {
            return new GenericResult { IsSuccess = true };
        }

        public static GenericResult Error(string errorCode, string msg)
        {
            return new GenericResult { ErrorCode = errorCode, Msg = msg, IsSuccess = false };
        }
    }
}
