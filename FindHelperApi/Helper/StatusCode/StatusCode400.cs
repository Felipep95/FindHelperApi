using System;

namespace FindHelperApi.Helper.CustomExceptions
{
    public class StatusCode400 :Exception
    {
        public StatusCode400(string message) : base(message)
        {

        }
    }
}
