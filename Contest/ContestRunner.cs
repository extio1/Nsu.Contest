namespace Nsu.Contest.Contest;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Nsu.Contest.Util;

public class ContestRunner : IHostedService
{
    readonly private IOptions<Configuration> _configuration;
    readonly private EmployeeReader _employeeReader;
    public ContestRunner(IOptions<Configuration> configuration, EmployeeReader employeeReader)
    {
        _configuration = configuration;
        _employeeReader = employeeReader;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        var teamleads = _employeeReader.ReadEmployee(_configuration.Value.TeamleadsPath);
        var juniors = _employeeReader.ReadEmployee(_configuration.Value.JuniorsPath);

        var n = 0;
        var avgHarmMean = 0.0;

        Console.WriteLine(_configuration.Value.NRounds);
        // for (var i = 0; i < _configuration.Value.nRounds; ++i)
        // {
        //     var harmMean = _contest.Run(teamleads, juniors);
        //     n++;
        //     avgHarmMean = (avgHarmMean * (n - 1) + harmMean) / n;
        //     Console.Write($"Harmonic mean: {harmMean}; Avg harmonic mean: {avgHarmMean}\n");
        // }

        return Task.CompletedTask;
    }
    public Task StopAsync(CancellationToken _)
    {
        return Task.CompletedTask;
    }

}


