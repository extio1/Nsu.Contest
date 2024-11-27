using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

using Nsu.Contest.Util;
using Nsu.Contest.Director;
using Nsu.Contest.Teambuilding;
using Nsu.Contest.Teambuilding.Strategy;
using Nsu.Contest.Database;
using Nsu.Contest.Entity;
using Nsu.Contest.CommandProcessor;
using Nsu.Contest.Contest;

class Program
{
    static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.Configure<ConfigurationContest>(context.Configuration.GetSection("Contest"));
                services.Configure<ConfigurationDatabase>(context.Configuration.GetSection("DatabaseProduction"));

                services.AddDbContext<ContestDbContext>(
                    options => 
                    options.UseNpgsql("Host=localhost;Port=5432;Database=contest;Username=extio1;Password="));
                services.AddScoped<ContestService>();
                services.AddScoped<EntityFactory>();

                services.AddScoped<ITeamBuildingStrategy, RandomTeamBuildingStrategy>();
                services.AddScoped<Manager>();
                services.AddScoped<WishlistGenerator>();
                services.AddSingleton<ITeamEstimatingStrategy, HarmonicMean>();
                services.AddSingleton<Director>();

                services.AddHostedService<CommandProcessor>();
            })
            .Build();
        host.Run();
    }

}
