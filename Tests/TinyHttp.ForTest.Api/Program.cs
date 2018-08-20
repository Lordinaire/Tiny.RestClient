﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Tiny.Http.ForTest.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).
                UseUrls("http://localhost:53095").
                Build().
                Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
