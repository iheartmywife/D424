using helpmepickmymain.Database;
using helpmepickmymain.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace helpmepickmymain.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly HmpmmDbContext hmpmmDbContext;

        public RoleRepository(HmpmmDbContext hmpmmDbContext)
        {
            this.hmpmmDbContext = hmpmmDbContext;
        }

        public async Task<Role> AddRoleAsync(Role role)
        {
            await hmpmmDbContext.Roles.AddAsync(role);
            await hmpmmDbContext.SaveChangesAsync();
            return role;
        }

        public async Task<Role?> DeleteRoleAsync(Guid id)
        {
            var existingRole = await hmpmmDbContext.Roles.FindAsync(id);

            if (existingRole != null)
            {
                hmpmmDbContext.Roles.Remove(existingRole);
                await hmpmmDbContext.SaveChangesAsync();
                return existingRole;
            }
            return null;
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await hmpmmDbContext.Roles.ToListAsync();
        }

        public async Task<Role?> GetRoleAsync(Guid id)
        {
            var debugRole = await hmpmmDbContext.Roles.FirstOrDefaultAsync(x => x.Id == id);
            return await hmpmmDbContext.Roles.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Role?> UpdateRoleAsync(Role role)
        {
            var existingRole = await hmpmmDbContext.Roles.FindAsync(role.Id);

            if (existingRole != null)
            {
                existingRole.Name = role.Name;
                existingRole.DisplayName = role.DisplayName;

                await hmpmmDbContext.SaveChangesAsync();

                return existingRole;
            }
            return null;
        }
    }
}
