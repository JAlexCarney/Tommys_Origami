using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Core
{
    public class Response
    {
        public bool Success { get => string.IsNullOrEmpty(Message); }
        public string Message { get; set; }
    }

    public class Response<T> : Response
    {
        public T Data { get; set; }
    }
}
