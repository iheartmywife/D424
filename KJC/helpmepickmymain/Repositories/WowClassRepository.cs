using helpmepickmymain.Database;
using helpmepickmymain.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace helpmepickmymain.Repositories
{
    public class WowClassRepository : IWowClassRepository
    {
        private readonly HmpmmDbContext hmpmmDbContext;

        public WowClassRepository(HmpmmDbContext hmpmmDbContext)
        {
            this.hmpmmDbContext = hmpmmDbContext;
        }
        public async Task<WowClass> AddWowClassAsync(WowClass wowClass)
        {
            await hmpmmDbContext.AddAsync(wowClass);
            await hmpmmDbContext.SaveChangesAsync();
            return wowClass;
        }

        public async Task<WowClass?> DeleteWowClassAsync(Guid id)
        {
            var existingWowClass = await hmpmmDbContext.WowClasses.FindAsync(id);

            if (existingWowClass != null)
            {
                hmpmmDbContext.WowClasses.Remove(existingWowClass);
                await hmpmmDbContext.SaveChangesAsync();
                return existingWowClass;
            }

            return null;
        }

        public async Task<IEnumerable<WowClass>> GetAllWowClassesAsync()
        {
            return await hmpmmDbContext.WowClasses
                .Include(x => x.Roles)
                .Include(x => x.Races)
                .Include(x => x.Specs)
                .ToListAsync();
        }

        public async Task<WowClass?> GetWowClassAsync(Guid id)
        {
            return await hmpmmDbContext.WowClasses
                .Include(x => x.Roles)
                .Include(x => x.Races)
                .Include(x => x.Specs)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<WowClass?> UpdateWowClassAsync(WowClass wowClass)
        {
            var existingWowClass = await hmpmmDbContext.WowClasses.FirstOrDefaultAsync(x => x.Id == wowClass.Id);

            if (existingWowClass != null)
            {
                existingWowClass.Name = wowClass.Name;
                existingWowClass.Id = wowClass.Id;
                existingWowClass.Races = wowClass.Races;
                existingWowClass.Roles = wowClass.Roles;
                existingWowClass.Specs = wowClass.Specs;

                await hmpmmDbContext.SaveChangesAsync();
                return existingWowClass;
            }
            return null;
        }
    }
}
