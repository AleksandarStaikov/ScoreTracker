﻿using Microsoft.Extensions.DependencyInjection;
using ScoreTracker.ConsoleRunner.Common;
using ScoreTracker.ConsoleRunner.Common.Interfaces;
using ScoreTracker.ConsoleRunner.Runner.Interfaces;

namespace ScoreTracker.ConsoleRunner.Runner;

public class CommandFactory : ICommandFactory
{
    private readonly Dictionary<string, Type> _availableCommands;
    private readonly IServiceProvider _serviceProvider;
    private readonly ICommunicationHub _communicationHub;

    public CommandFactory(IServiceProvider serviceProvider, ICommunicationHub communicationHub)
    {
        _availableCommands = CommandLocator
            .GetAllCommandsInAssembly()
            .ToDictionary(type => type.Name.ToLower(), type => type);

        _serviceProvider = serviceProvider;
        _communicationHub = communicationHub;
    }

    public ICommand? ResolveCommand(string commandName)
    {
        if (!_availableCommands.TryGetValue(commandName, out var commandType))
        {
            _communicationHub.PublichMessage(new Message($"Command '{commandName}' not found! Type 'help' to get more information", MessageSeverity.Error));
        }

        return _serviceProvider.GetRequiredService(commandType!) as ICommand;       
    }
}