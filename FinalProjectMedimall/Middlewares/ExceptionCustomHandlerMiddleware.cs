using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using FinalProjectMedimall.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace FinalProjectMedimall.Middlewares
{
    public class ExceptionCustomHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionCustomHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);

            }
            catch (Exception ex)
            {

                await HandleException(context, ex);
                //await
            }
        }
        public async Task HandleException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var model = new ErrorModel
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            };
            context.Request.Headers.Add("error", JsonConvert.SerializeObject(model));
            context.Response.Redirect("/Home/Error");
            await context.Response.WriteAsync(model.ToString());
        }
    }
}