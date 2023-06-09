﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScoreTracker.Common.Extensions;
using ScoreTracker.ConsoleRunner.Common.Communication;
using ScoreTracker.ConsoleRunner.Common.Interfaces;
using ScoreTracker.ConsoleRunner.Imports;
using ScoreTracker.ConsoleRunner.Imports.Interfaces;
using ScoreTracker.ConsoleRunner.Runner;
using ScoreTracker.ConsoleRunner.Runner.Interfaces;
using ScoreTracker.Data;
using System.Reflection;

namespace ScoreTracker.ConsoleRunner;

public static class DependencyInjectionProvider
{
    public static IServiceProvider GetServiceProvider()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.Development.json", false)
       .Build();

        var services = new ServiceCollection()
            .ExternalDependencies(configuration)
            .AddTransient<IDataSeeder, DataSeeder>()
            .AddAsSingleton<IImporter>(typeof(DependencyInjectionProvider).Assembly)
            .AddSingleton<IConsoleWritter, ConsoleWritter>()
            .AddSingleton<ICommandFactory, CommandFactory>()
            .AddSingleton<ICommunicationHub, CommunicationHub>()
            .AddSingleton<IRunner, Runner.Runner>();

        foreach (var command in CommandLocator.GetAllCommandsInAssembly())
        {
            services.AddTransient(command);
        }


        IServiceProvider serviceProvider = services.BuildServiceProvider();

        return serviceProvider;
    }

    public static IServiceCollection ExternalDependencies(this IServiceCollection services, IConfigurationRoot configuration)
        => services.AddData(configuration)
            .AddScoreTracker(configuration)
            .AddLogging();
}
