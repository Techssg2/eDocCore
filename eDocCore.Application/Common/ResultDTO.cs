using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDocCore.Application.Common
{
    public class ResultDTO<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> ErrorMessages { get; set; } = new List<string>();
        public T? Data { get; set; }

        public static ResultDTO<T> Success(T data, string message = null)
        {
            return new ResultDTO<T> { IsSuccess = true, Data = data, Message = message };
        }

        public static ResultDTO<T> Failure(List<string> errorMessages)
        {
            return new ResultDTO<T> { IsSuccess = false, Message = "Failure" , ErrorMessages = errorMessages };
        }
    }
}
