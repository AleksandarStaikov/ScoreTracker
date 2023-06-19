using ScoreTracker.Common.Models.User;
using ScoreTracker.ConsoleRunner.Common.Communication;
using ScoreTracker.ConsoleRunner.Common.Interfaces;
using ScoreTracker.ConsoleRunner.Imports.Interfaces;
using ScoreTracker.Interfaces;
using System.Text;

namespace ScoreTracker.ConsoleRunner.Imports.User;

public class UserImporter : IImporter
{
    private const string ImportRandomArgument = "random-count";
    private const string UsernameArgument = "username";
    private const string ImportFromFileArgument = "file-name";

    private readonly ICommunicationHub _communicationHub;
    private readonly IUserService _userService;

    public UserImporter(ICommunicationHub communicationHub, IUserService userService)
    {
        _communicationHub = communicationHub;
        _userService = userService;
    }

    public bool CanImport(CommandBody commandBody)
        => commandBody.HasPositionalArgument("user", 0);

    public void Import(CommandBody commandBody)
    {
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

        if (commandBody.HasNamedArgument(UsernameArgument, out string? username))
        {
            return new[] { new CreateUserDto { AuthId = username!, Username = username! } };
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
}