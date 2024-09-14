using helpmepickmymain.Models.Domain;

namespace helpmepickmymain.Repositories
{
    public interface ISpecRepository
    {
        Task<IEnumerable<Spec>> GetAllSpecsAsync();

        Task<IEnumerable<Spec>> GetAllSpecsWithNameAsync(string Name);

        Task<Spec?> GetSpecAsync(Guid id);

        Task<Spec> AddSpecAsync(Spec spec);

        Task<Spec?> UpdateSpecAsync(Spec Spec);

        Task<Spec?> DeleteSpecAsync(Guid id);
    }
}
