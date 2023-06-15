using ScoreTracker.Common.Models.User;
using ScoreTracker.ConsoleRunner.Common;
using ScoreTracker.ConsoleRunner.Common.Interfaces;
using ScoreTracker.ConsoleRunner.Imports.Interfaces;
using ScoreTracker.Interfaces;
using System.Text;

namespace ScoreTracker.ConsoleRunner.Imports.User;

public class UserImporter : IImporter
{
    private const string ImportRandomArgument = "random-count";
    private const string ImportFromFileArgument = "file-name";

    private readonly ICommunicationHub _communicationHub;
    private readonly IUserService _userService;

    public UserImporter(ICommunicationHub communicationHub, IUserService userService)
    {
        _communicationHub = communicationHub;
        _userService = userService;
    }


    public void Import(CommandBody commandBody)
    {
        if (!ParametersValid(commandBody)) return;

        var dataSetToImport = GetDataSet(commandBody);

        var importTask = ImportDataSet(dataSetToImport);

        importTask.Wait();
    }

    private IEnumerable<CreateUserDto> GetDataSet(CommandBody commandBody)
    {

        if (commandBody.HasNamedArgument(ImportRandomArgument, out string? numberOfUsersToimport))
        {
            var n = int.Parse(numberOfUsersToimport!);

            return GenerateRandomUsers(n);
        }

        if (commandBody.HasNamedArgument(ImportFromFileArgument, out string? filename))
        {
            return ImportUsersFromFile(filename!);
        }

        return Enumerable.Empty<CreateUserDto>();   
    }

    private IEnumerable<CreateUserDto> ImportUsersFromFile(string filename)
    {
        throw new NotImplementedException();
    }

    private IEnumerable<CreateUserDto> GenerateRandomUsers(int count)
    {
        var users = new CreateUserDto[count];

        for (int i = 0; i < count; i++)
        {
            var identifier = $"rnd-import-{Guid.NewGuid()}";

            users[i] = new CreateUserDto()
            {
                AuthId = identifier,
                Username = identifier
            };
        }

        return users;
    }

    private async Task ImportDataSet(IEnumerable<CreateUserDto> usersToCreate)
    {
        var sb = new StringBuilder();

        foreach (var userDto in usersToCreate)
        {
            await _userService.CreateUser(userDto);

            sb.AppendLine($"Created user '{userDto.Username}'");
        }

        _communicationHub.PublichMessage(sb.ToString(), MessageSeverity.Success);
    }

    private bool ParametersValid(CommandBody commandBody)
    {
        if (HasBothImportControllFlags(commandBody) ||
            HasNoneImportControllFlags(commandBody))
        {
            _communicationHub.PublichMessage("You must speficy either a '--file-name <name-of-file>' file to import or use the -r flag that will generate random users");
            return false;
        }

        return true;
    }

    private static bool HasBothImportControllFlags(CommandBody commandBody)
        => commandBody.HasNamedArgument(ImportRandomArgument) &&
            commandBody.HasNamedArgument(ImportFromFileArgument);

    private bool HasNoneImportControllFlags(CommandBody commandBody)
        => !commandBody.HasNamedArgument(ImportRandomArgument) &&
            !commandBody.HasNamedArgument(ImportFromFileArgument);

}