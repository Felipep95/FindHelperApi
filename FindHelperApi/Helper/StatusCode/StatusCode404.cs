using System;

namespace FindHelperApi.Helper.StatusCode
{
    public class StatusCode404 : Exception
    {
        public StatusCode404(string message) : base(message)
        {

        }
    }
}
