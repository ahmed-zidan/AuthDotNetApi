﻿using AuthApi.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
//using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace AuthApi.Middlewares
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IWebHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next,
            ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this._env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                ApiResponse apiError;
                HttpStatusCode statusCode;
                string message;
                var exType = ex.GetType();
                if (exType == typeof(UnauthorizedAccessException))
                {
                    statusCode = HttpStatusCode.Forbidden;
                    message = "you are not allowed";
                }
                else
                {
                    statusCode = HttpStatusCode.InternalServerError;
                    message = ex.Message;
                }

                if (_env.IsDevelopment())
                {
                    apiError = new ApiResponse((int)statusCode, message, ex.StackTrace.ToString());
                }
                else
                {
                    apiError = new ApiResponse((int)statusCode, message);
                }
                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = (int)statusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(apiError));
            }
        }


    }


}
