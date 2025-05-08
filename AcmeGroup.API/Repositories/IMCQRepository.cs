using AcmeGroup.API.Models.Domain;

namespace AcmeGroup.API.Repositories
{
    public interface IMCQRepository
    {
       Task<List<MCQ>> GetAllAsync();

       Task<MCQ?> GetByIdAsync(Guid id);

       Task<MCQ> CreateAsync(MCQ mCQ);

       Task<MCQ?> UpdateAsync(Guid id, MCQ mCQ);

       Task<string?> DeleteAsync(Guid id);
    }
}
