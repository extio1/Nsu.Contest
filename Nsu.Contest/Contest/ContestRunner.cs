namespace Nsu.Contest.Contest;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Nsu.Contest.Teambuilding;
using Nsu.Contest.Director;
using Nsu.Contest.Util;
using Nsu.Contest.Entity;

public class ContestRunner : IHostedService
{
    readonly private IOptions<ConfigurationContest> _configuration;
    readonly private EmployeeReader _employeeReader;
    readonly private Director _director;
    readonly private Manager _manager;
    readonly private WishlistGenerator _wishlistGenerator;
    public ContestRunner(IOptions<ConfigurationContest> configuration, EmployeeReader employeeReader,
                         Director director, Manager manager, WishlistGenerator wishlistGenerator)
    {
        _wishlistGenerator = wishlistGenerator;
        _director = director;
        _manager = manager;
        _configuration = configuration;
        _employeeReader = employeeReader;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var teamleads = _employeeReader.ReadEmployees<Teamlead>(_configuration.Value.TeamleadsPath);
        var juniors = _employeeReader.ReadEmployees<Junior>(_configuration.Value.JuniorsPath);

        var tasks = new List<Task<double>>(_configuration.Value.NRounds);
        for (var i = 0; i < _configuration.Value.NRounds; i++) {
            tasks.Add(
                Task.Run(() => new Contest(_director, _manager, _wishlistGenerator).Run(teamleads, juniors))
            );
        }

        PrintAvgHarmToConsole(await Task.WhenAll(tasks));
    }
    public Task StopAsync(CancellationToken _)
    {
        return Task.CompletedTask;
    }

    private void PrintAvgHarmToConsole(double[] scores) 
    {
        Console.WriteLine($"Average harmonic for {_configuration.Value.NRounds} rounds is {scores.Sum() / scores.Length}");
    }

}


