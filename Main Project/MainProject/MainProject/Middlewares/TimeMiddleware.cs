using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MainProject.Middlewares
{
    public class TimeMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public TimeMiddleware(RequestDelegate next, ILogger<TimeMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var sw = new Stopwatch();
            sw.Start();
            await _next(context);

            _logger.LogInformation($"Executed in {sw.ElapsedMilliseconds}ms");
            Console.WriteLine($"Executed in {sw.ElapsedMilliseconds}ms");
        }
    }
}
