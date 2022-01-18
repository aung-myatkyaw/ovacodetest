using System;
using System.Net.Http;

namespace OvaCodeTest.Exceptions
{
    class HttpRequestExceptionEx: HttpRequestException
    {
        public System.Net.HttpStatusCode HttpCode { get; }
        public HttpRequestExceptionEx(System.Net.HttpStatusCode code) : this(code, null, null)
        {
        }

        public HttpRequestExceptionEx(System.Net.HttpStatusCode code, string message) : this(code, message, null)
        {
            HttpCode = code;
        }

        public HttpRequestExceptionEx(System.Net.HttpStatusCode code, string message, Exception inner) : base(message,inner)
        {
            HttpCode = code;
        }

    }
}
