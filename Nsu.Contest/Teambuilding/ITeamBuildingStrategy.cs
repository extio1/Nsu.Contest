namespace Nsu.Contest.Teambuilding;

using Nsu.Contest.Entity;

public interface ITeamBuildingStrategy
{
    /// <summary>
    /// Распределяет тимлидов и джунов по командам
    /// </summary>
    /// <param name="teamLeads">Тимлиды</param>
    /// <param name="juniors">Джуны</param>
    /// <returns>Список команд</returns>
    IEnumerable<Team> BuildTeams(
        IEnumerable<Employee> teamleads, IEnumerable<Employee> juniors,
        IEnumerable<Wishlist> teamleadsWishlists, IEnumerable<Wishlist> juniorsWishlists
    );
}
