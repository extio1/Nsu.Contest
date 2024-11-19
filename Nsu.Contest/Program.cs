using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Nsu.Contest.Util;
using Nsu.Contest.Director;
using Nsu.Contest.Teambuilding;
using Nsu.Contest.Teambuilding.Strategy;
using Nsu.Contest.Contest;
using Nsu.Contest.CommandProcessor;

class Program
{
    static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.Configure<ConfigurationContest>(context.Configuration.GetSection("Contest"));
                services.Configure<ConfigurationDatabase>(context.Configuration.GetSection("DatabaseProduction"));

                services.AddSingleton<ITeamBuildingStrategy, RandomTeamBuildingStrategy>();
                services.AddSingleton<ITeamEstimatingStrategy, HarmonicMean>();
                services.AddSingleton<WishlistGenerator>();
                services.AddSingleton<EmployeeReader>();
                services.AddSingleton<Director>();
                services.AddSingleton<Manager>();

                services.AddHostedService<ContestRunner>();
                // services.AddHostedService<CommandProcessor>();
            })
            .Build();

        host.Run();
    }

}
