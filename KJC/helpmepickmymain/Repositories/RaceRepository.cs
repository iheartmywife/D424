using helpmepickmymain.Database;
using helpmepickmymain.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace helpmepickmymain.Repositories
{
    public class RaceRepository : IRaceRepository
    {
        private readonly HmpmmDbContext hmpmmDbContext;

        public RaceRepository(HmpmmDbContext hmpmmDbContext)
        {
            this.hmpmmDbContext = hmpmmDbContext;
        }
        public async Task<Race> AddRaceAsync(Race race)
        {
            await hmpmmDbContext.AddAsync(race);
            await hmpmmDbContext.SaveChangesAsync();
            return race;
        }

        public async Task<Race?> DeleteRaceAsync(Guid id)
        {
            var existingRace = await hmpmmDbContext.Races.FindAsync(id);

            if (existingRace != null)
            {
                hmpmmDbContext.Races.Remove(existingRace);
                await hmpmmDbContext.SaveChangesAsync();
                return existingRace;
            }

            return null;
        }

        public async Task<IEnumerable<Race>> GetAllRacesAsync()
        {


            return await hmpmmDbContext.Races
                .Include(x => x.Faction)
                .Include(x => x.WowClasses)
                .ToListAsync();
        }

        public async Task<Race?> GetRaceAsync(Guid id)
        {
            return await hmpmmDbContext.Races
                .Include(x => x.Faction)
                .Include(x => x.WowClasses)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Race?> UpdateRaceAsync(Race race)
        {
            var existingRace = await hmpmmDbContext.Races
                .Include(x => x.Faction)
                .Include(x => x.WowClasses)
                .FirstOrDefaultAsync(x => x.Id == race.Id);

            if (existingRace != null)
            {
                existingRace.Id = race.Id;
                existingRace.Name = race.Name;
                existingRace.Faction = race.Faction;
                existingRace.WowClasses = race.WowClasses;

                await hmpmmDbContext.SaveChangesAsync();
                return existingRace;
            }
            return null;
        }
    }
}
