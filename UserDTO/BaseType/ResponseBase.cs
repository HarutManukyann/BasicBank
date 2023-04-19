using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.BaseType
{
    public class ResponseBase<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

        public ResponseBase(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
        public ResponseBase(bool success, string message)
        {
            Success = success;
            Message = message;           
        }
        public ResponseBase()
        {

        }
    }
}
