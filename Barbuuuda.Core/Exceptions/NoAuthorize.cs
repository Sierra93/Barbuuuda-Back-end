using System;
using System.Net;

namespace Barbuuuda.Core.Exceptions
{
    public class NoAuthorize : Exception
    {
        public HttpStatusCode Status { get; private set; }

        public NoAuthorize(HttpStatusCode status, string message) : base(message)
        {
            Status = status;
        }

        //public NoAuthorize() : base("Пользователь с таким логином уже существует") { }

        //public NoAuthorize(string message, Exception innerException) : base(message, innerException) { }
    }
}
