using System;
using System.Net;

namespace FindHelperApi.Helper
{
    [Serializable]
    public class Exceptions : Exception //TODO: criar exceptions customizadas para tratar erros dos services
        //https://docs.microsoft.com/en-us/aspnet/web-api/overview/error-handling/exception-handling
    {
        public Exceptions() { }

        public Exceptions(string errorMessage)
            : base(errorMessage) { }

        //public Exceptions(HttpStatusCode statusCode, string errorMessage)
        //    : base(statusCode, errorMessage) { }

    }
}
