using helpmepickmymain.Database;
using helpmepickmymain.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace helpmepickmymain.Repositories
{
    public class SpecRepository : ISpecRepository
    {
        private readonly HmpmmDbContext hmpmmDbContext;

        public SpecRepository(HmpmmDbContext hmpmmDbContext)
        {
            this.hmpmmDbContext = hmpmmDbContext;
        }
        public async Task<Spec> AddSpecAsync(Spec spec)
        {
            await hmpmmDbContext.AddAsync(spec);
            await hmpmmDbContext.SaveChangesAsync();
            return spec;
        }

        public async Task<Spec?> DeleteSpecAsync(Guid id)
        {
            var existingSpec = await hmpmmDbContext.Specs.FindAsync(id);

            if (existingSpec != null)
            {
                hmpmmDbContext.Specs.Remove(existingSpec);
                await hmpmmDbContext.SaveChangesAsync();
                return existingSpec;
            }

            return null;
        }

        public async Task<IEnumerable<Spec>> GetAllSpecsAsync()
        {
            return await hmpmmDbContext.Specs
                .Include(x => x.Role)
                .Include(x => x.WowClass)
                .ToListAsync();
        }

        public async Task<Spec?> GetSpecAsync(Guid id)
        {
            return await hmpmmDbContext.Specs
                .Include(x => x.Role)
                .Include(x => x.WowClass)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Spec?> UpdateSpecAsync(Spec Spec)
        {
            var existingSpec = await hmpmmDbContext.Specs
                .Include(x => x.Role)
                .Include(x => x.WowClass)
                .FirstOrDefaultAsync(x => x.Id ==  Spec.Id);

            if (existingSpec != null)
            {
                existingSpec.Id = Spec.Id;
                existingSpec.Name = Spec.Name;
                existingSpec.Role = Spec.Role;
                existingSpec.WowheadLink = Spec.WowheadLink;
                existingSpec.WowClass = Spec.WowClass;

                await hmpmmDbContext.SaveChangesAsync();
                return existingSpec;
            }
            return null;
        }
    }
}
