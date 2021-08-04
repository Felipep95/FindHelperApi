using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FindHelperApi.Helper.StatusCode;
using Microsoft.AspNetCore.Http;

//https://brunomj.medium.com/net-5-0-web-api-global-error-handling-6c29f91f8951

namespace FindHelperApi.Helper.CustomExceptions
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch(ex)
                {
                    case StatusCode400:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case StatusCode404:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                }

                var errorResponse = new
                {
                    message = ex.Message,
                    //statusCode = response.StatusCode
                };

                var errorJson = JsonSerializer.Serialize(errorResponse);

                await response.WriteAsync(errorJson);
            }
        }
    }
}
