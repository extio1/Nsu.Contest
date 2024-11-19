namespace Nsu.Contest.CommandProcessor;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Nsu.Contest.Contest;
using Nsu.Contest.Director;
using Nsu.Contest.Database;
using Nsu.Contest.Teambuilding;
using Nsu.Contest.Util;

public class CommandProcessor : IHostedService
{
    private readonly IServiceProvider _services;
    private readonly CancellationTokenSource _cancellationTokenSource;
    readonly private ContestDataService _contestDataService;
    readonly private IOptions<ConfigurationContest> _configuration;
    readonly private EmployeeReader _employeeReader;
    readonly private Director _director;
    readonly private Manager _manager;
    readonly private WishlistGenerator _wishlistGenerator;

    public CommandProcessor(IServiceProvider services)
    {
        _services = services;
        _cancellationTokenSource = new();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(() => ProcessCommands(_cancellationTokenSource.Token), cancellationToken);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private Task ProcessCommands(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            Console.Write("> ");
            var commandString = Console.ReadLine();
            if (commandString == null)
                continue;

            var command = commandString.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (command.Length == 0 || string.IsNullOrWhiteSpace(command[0]))
                continue;

            using var scope = _services.CreateScope();

            switch (command[0].ToLower())
            {
                case "run":
                    RunContest();
                    break;
                case "print":
                    if(command.Length < 2)
                    {
                        Console.WriteLine("Для команды print необходимо указать идентификатор соревнования.");
                        break;
                    }

                    try
                    {
                        var id = long.Parse(command[1]);
                        PrintInfoByContestId(id);
                    }
                    catch (FormatException){
                        Console.WriteLine("Неверный формат идентификатора соревнования.");
                        break;
                    }

                    break;
                case "avharm":
                    PrintAvgHarmonicForAllContests();
                    break;
                default:
                    Console.WriteLine("Неизвестная команда.");
                    break;
            }
        }

        return Task.CompletedTask;
    }

    private void RunContest(){
        Console.WriteLine("Запуск Contest...");
        var teamleads = _contestDataService.GetTeamleads();
        var juniors = _contestDataService.GetJuniours();

        var contest = new Contest(_director, _manager, _wishlistGenerator);
        var point = contest.Run(teamleads, juniors);
    } 

    private void PrintInfoByContestId(long id){
        Console.WriteLine($"Запуск print с id={id}...");
    }

    private void PrintAvgHarmonicForAllContests(){
        Console.WriteLine("Запуск avharm...");
    }
}

