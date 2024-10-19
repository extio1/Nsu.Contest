namespace Nsu.Contest.Contest;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Nsu.Contest.Teambuilding;
using Nsu.Contest.Director;
using Nsu.Contest.Util;
using System.Runtime.CompilerServices;

public class ContestRunner : IHostedService
{
    readonly private IOptions<Configuration> _configuration;
    readonly private EmployeeReader _employeeReader;
    readonly private Director _director;
    readonly private Manager _manager;
    readonly private RandomGenerator _randomGenerator;
    public ContestRunner(IOptions<Configuration> configuration, EmployeeReader employeeReader,
                         Director director, Manager manager, RandomGenerator randomGenerator)
    {
        _randomGenerator = randomGenerator;
        _director = director;
        _manager = manager;
        _configuration = configuration;
        _employeeReader = employeeReader;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var teamleads = _employeeReader.ReadEmployee(_configuration.Value.TeamleadsPath);
        var juniors = _employeeReader.ReadEmployee(_configuration.Value.JuniorsPath);

        var tasks = new List<Task<double>>(_configuration.Value.NRounds);
        for (var i = 0; i < _configuration.Value.NRounds; i++) {
            // teamleads и juniors передаются во все методы Contest::Run(...) по ссылке же?
            // Вопрос к тому, что контесты у меня по факту только читают списки тимлидов и джунов
            // т.е. не конкурируют за них -> взаимное исключение или копирование не обязательно 
            tasks.Add(
                Task.Run(() => new Contest(_director, _manager, _randomGenerator).Run(teamleads, juniors))
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


