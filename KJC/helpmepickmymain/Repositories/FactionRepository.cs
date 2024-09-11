using helpmepickmymain.Database;
using helpmepickmymain.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace helpmepickmymain.Repositories
{
    public class FactionRepository : IFactionRepository
    {
        private readonly HmpmmDbContext hmpmmDbContext;

        public FactionRepository(HmpmmDbContext hmpmmDbContext)
        {
            this.hmpmmDbContext = hmpmmDbContext;
        }
        public async Task<Faction> AddFactionAsync(Faction faction)
        {
            await hmpmmDbContext.AddAsync(faction);
            await hmpmmDbContext.SaveChangesAsync();
            return faction;
        }

        public async Task<Faction?> DeleteFactionAsync(Guid id)
        {
            var existingFaction = await hmpmmDbContext.Factions.FindAsync(id);

            if (existingFaction != null)
            {
                hmpmmDbContext.Factions.Remove(existingFaction);
                await hmpmmDbContext.SaveChangesAsync();
                return existingFaction;
            }

            return null;
        }

        public async Task<IEnumerable<Faction>> GetAllFactionsAsync()
        {
            return await hmpmmDbContext.Factions
                .Include(x => x.Races)
                .ToListAsync();
        }

        public async Task<Faction?> GetFactionAsync(Guid id)
        {
            return await hmpmmDbContext.Factions
                .Include(x => x.Races)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Faction?> UpdateFactionAsync(Faction faction)
        {
            var existingFaction = await hmpmmDbContext.Factions.FirstOrDefaultAsync(x => x.Id == faction.Id);

            if (existingFaction != null)
            {
                existingFaction.Name = faction.Name;
                existingFaction.Id = faction.Id;
                existingFaction.Races = faction.Races;

                await hmpmmDbContext.SaveChangesAsync();
                return existingFaction;
            }
            return null;
        }
    }
}
