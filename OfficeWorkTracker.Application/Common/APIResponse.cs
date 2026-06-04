using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWorkTracker.Application.Common
{
    public class APIResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
    }

    public static class ApiResponseFactory
    {
        public static APIResponse<T> Success<T>(T data, string message)
        {
            return new APIResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };

        }

        public static APIResponse<T> Failure<T>(string message)
        {
            return new APIResponse<T>
            {
                Success = false,
                Message = message,
                Data = default
            };
        }
    }

}
