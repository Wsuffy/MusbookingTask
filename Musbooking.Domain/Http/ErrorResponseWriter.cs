using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;
using Musbooking.Domain.Http.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Musbooking.Domain.Http
{
    public static class ErrorResponseWriter
    {
        public static async Task WriteResponseToContextAsync(this HttpContext context, ErrorResponse errorResponse,
            HttpStatusCode? statusCode = HttpStatusCode.Forbidden)
        {
            context.Response.StatusCode = (int)statusCode!;
            context.Response.ContentType = "application/json";
            var responseJson = JsonConvert.SerializeObject(errorResponse, JsonSerializerSettings);
            await context.Response.WriteAsync(responseJson,
                Encoding.UTF8,
                context.RequestAborted);
        }

        private static readonly JsonSerializerSettings JsonSerializerSettings = new()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
        };
    }
}