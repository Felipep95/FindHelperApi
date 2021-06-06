using FindHelperApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Web.Http.ModelBinding;

//https://stackoverflow.com/questions/38630076/asp-net-core-web-api-exception-handling

namespace FindHelperApi.Helper.CustomExceptions
{
    //to test
    public class MyErrorResponse
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }

        public MyErrorResponse(Exception ex)
        {
            Type = ex.GetType().Name;
            Message = ex.Message;
            StackTrace = ex.ToString();
        }
    }

    // TODO: Implements status code in return of errorMessage
    public class HttpStatusCodeException : Exception
    {
        public HttpStatusCode Status { get; private set; }

        public HttpStatusCodeException(HttpStatusCode status, string msg) : base(msg)
        {
            Status = status;
        }
    }


    public class CustomValidationModel : ControllerBase
    {
        public static bool CheckPasswordLength(string password) //This method return the same json as validation model... then is not necessary create a custom class exception to handle errors.
        {
            var modelState = new ModelStateDictionary();
            //var statusCode = new HttpStatusCode();

            if (password.Length < 8)
            {
                modelState.AddModelError(nameof(password), "A senha precisa ter no mínimo 8 caracteres.");
                //return modelState.Values.
            }

            return modelState.IsValid;
        }
    }
}
