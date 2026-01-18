using System;
using FormatterLibrary.Interfaces;
using FormatterLibrary.Models;
using FormatterLibrary.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace FormatterLibrary.Extension;

public static class ServiceCollectionExtension
{
    /// <summary>
    /// Service that registers all formatters
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddCustomFormatter(this IServiceCollection services)
    {
        //Register json formatter
        services.AddScoped<IResponseFormatter, JsonResponseFormatter>();
        //Register xml formatter
        services.AddScoped<IResponseFormatter, XmlResponseFormatter>();
        //Register yaml formatter
        services.AddScoped<IResponseFormatter, YamlResponseFormatter>();
        //Register csv formatter
        services.AddScoped<IResponseFormatter, CsvResponseFormatter>();
        //Register message pack formatter
        services.AddScoped<IResponseFormatter, MessagePackResponseFormatter>();
        //Register protobuf formatter
        services.AddScoped<IResponseFormatter, ProtobufResponseFormatter>();
        
        services.AddScoped<FormatterPipeLine>();

        return services;
    }


}
