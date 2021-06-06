using System.Net;
using WebApi;

namespace FindHelperApi.Helper.Tests.StatusCodesExceptionsErrors
{
    public class FormatInvalid : ApiException
    {
        public FormatInvalid()
            : base(HttpStatusCode.BadRequest)
        {

        }
    }
}
