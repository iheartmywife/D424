using helpmepickmymain.Models.Domain;

namespace helpmepickmymain.Repositories
{
    public interface IRaceRepository
    {
        Task<IEnumerable<Race>> GetAllRacesAsync();

        Task<Race?> GetRaceAsync(Guid id);

        Task<Race> AddRaceAsync(Race race);

        Task<Race?> UpdateRaceAsync(Race race);

        Task<Race?> DeleteRaceAsync(Guid id);
    }
}
