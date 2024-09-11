using helpmepickmymain.Models.Domain;

namespace helpmepickmymain.Repositories
{
    public interface IWowClassRepository
    {
        Task<IEnumerable<WowClass>> GetAllWowClassesAsync();

        Task<WowClass?> GetWowClassAsync(Guid id);

        Task<WowClass> AddWowClassAsync(WowClass wowClass);

        Task<WowClass?> UpdateWowClassAsync(WowClass wowClass);

        Task<WowClass?> DeleteWowClassAsync(Guid id);
    }
}
