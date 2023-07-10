﻿using Serilog.Sinks.Elasticsearch;
using Serilog.Exceptions;
using System.Reflection;
using Serilog;

namespace MeetUp.CommentsService.Api.Features
{
    public class LoggerConfigurator
    {
        public static void ConfigureLog(IConfigurationRoot configuration)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            Log.Logger = new LoggerConfiguration()
                        .Enrich.FromLogContext()
                        .Enrich.WithExceptionDetails()
                        .WriteTo.Debug()
                        .WriteTo.Elasticsearch(ConfigureELK(configuration, env!))
                        .CreateLogger();
        }

        private static ElasticsearchSinkOptions ConfigureELK(
            IConfigurationRoot configuration,
            string env)
        {
            return new ElasticsearchSinkOptions(new Uri(configuration["ELKConfiguration:Uri"]!))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name!.ToLower()}-{env.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
            };
        }
    }
}
