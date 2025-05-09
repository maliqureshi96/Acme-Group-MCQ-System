using AcmeGroup.API.Models.Domain;

namespace AcmeGroup.API.Repositories
{
    public interface IMCQRepository
    {
       Task<List<MCQ>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);

       Task<MCQ?> GetByIdAsync(Guid id);

       Task<MCQ> CreateAsync(MCQ mCQ);

       Task<MCQ?> UpdateAsync(Guid id, MCQ mCQ);

       Task<string?> DeleteAsync(Guid id);
    }
}
