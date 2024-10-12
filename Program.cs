using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Nsu.Contest.Util;
using Nsu.Contest.Contest;
using Nsu.Contest.Director;
using Nsu.Contest.Teambuilding;
using Nsu.Contest.Teambuilding.Strategy;

class Program
{
    static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.Configure<Configuration>(context.Configuration.GetSection("Contest"));

                services.AddSingleton<ITeamBuildingStrategy, RandomTeamBuildingStrategy>();
                services.AddSingleton<RandomGenerator>();
                services.AddSingleton<EmployeeReader>();
                services.AddSingleton<Director>();
                services.AddSingleton<Manager>();

                services.AddHostedService<ContestRunner>();
            })
            .Build();

        host.Run();
    }

}
