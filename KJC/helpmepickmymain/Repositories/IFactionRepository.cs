using helpmepickmymain.Models.Domain;

namespace helpmepickmymain.Repositories
{
    public interface IFactionRepository
    {
        Task<IEnumerable<Faction>> GetAllFactionsAsync();

        Task<Faction?> GetFactionAsync(Guid id);

        Task<Faction> AddFactionAsync(Faction faction);

        Task<Faction?> UpdateFactionAsync(Faction faction);

        Task<Faction?> DeleteFactionAsync(Guid id);
    }
}
