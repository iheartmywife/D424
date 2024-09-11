using helpmepickmymain.Models.Domain;

namespace helpmepickmymain.Repositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllRolesAsync();

        Task<Role?> GetRoleAsync(Guid id);

        Task<Role> AddRoleAsync(Role role);

        Task<Role?> UpdateRoleAsync(Role role);

        Task<Role?> DeleteRoleAsync(Guid id);
    }
}
