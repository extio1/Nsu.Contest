namespace Nsu.Contest.CommandProcessor;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Nsu.Contest.Contest;
using Nsu.Contest.Database;
using Nsu.Contest.Director;
using Nsu.Contest.Entity;
using Nsu.Contest.Teambuilding;
using Nsu.Contest.Util;

public class CommandProcessor : IHostedService
{
    private readonly IServiceProvider _services;
    private readonly CancellationTokenSource _cancellationTokenSource;
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

    // scope - пока жив процесс 
    private Task ProcessCommands(CancellationToken token)
    {
        using (var scope = _services.CreateScope())
        {
            var contestService = scope.ServiceProvider.GetRequiredService<ContestService>();
            while (!token.IsCancellationRequested)
            {
                Console.Write("> ");
                var commandString = Console.ReadLine();
                if (commandString == null)
                    continue;

                var command = commandString.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (command.Length == 0 || string.IsNullOrWhiteSpace(command[0]))
                    continue;

                switch (command[0].ToLower())
                {
                    case "run":
                        contestService.MakeContest();
                        break;
                    case "print":
                        try
                        {
                            contestService.LogToConsoleContestInfo(Guid.Parse(command[1]));
                        }
                        catch (FormatException){
                            Console.WriteLine("Неверный формат идентификатора соревнования.");
                            break;
                        }
                        catch (IndexOutOfRangeException){
                            Console.WriteLine("Для команды print необходимо указать идентификатор соревнования.");
                            break;
                        }
                        break;
                    case "avharm":
                        var result = contestService.CalculateHarmonicMeanForAll();
                        Console.WriteLine($"Average harmonic for all rounds is {result}");
                        break;
                    default:
                        Console.WriteLine("Неизвестная команда.");
                        break;
                }
            }
        }

        return Task.CompletedTask;
    }
}

