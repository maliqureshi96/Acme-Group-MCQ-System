using AcmeGroup.API.Data;
using AcmeGroup.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AcmeGroup.API.Repositories
{
    public class SQLMCQRepository : IMCQRepository
    {
        private readonly AcmeDbContext dbContext;

        public SQLMCQRepository(AcmeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<MCQ> CreateAsync(MCQ mCQ)
        {
            await dbContext.MCQs.AddAsync(mCQ);
            await dbContext.SaveChangesAsync();
            return mCQ;
        }

        public async Task<string?> DeleteAsync(Guid id)
        {
            var existingMCQ = await dbContext.MCQs.FirstOrDefaultAsync(x => x.Id == id);
            if (existingMCQ == null)
            {
                return null;
            }

            dbContext.MCQs.Remove(existingMCQ);
            await dbContext.SaveChangesAsync();

            return "MCQ Deleted";
        }

        public async Task<List<MCQ>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var mcqs = dbContext.MCQs.AsQueryable();

            // Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Question", StringComparison.OrdinalIgnoreCase))
                {
                    mcqs = mcqs.Where(x => x.Question.Contains(filterQuery));
                }
  
            }

            // Sorting
            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("McqNumber"))
                {
                    mcqs = isAscending ? mcqs.OrderBy(x => x.McqNumber): mcqs.OrderByDescending(x => x.McqNumber);
                }
                else if (sortBy.Equals("Question", StringComparison.OrdinalIgnoreCase))
                {
                    mcqs = isAscending ? mcqs.OrderBy(x => x.Question) : mcqs.OrderByDescending(x => x.Question);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;



            return await mcqs.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<MCQ?> GetByIdAsync(Guid id)
        {
            return await dbContext.MCQs.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<MCQ?> UpdateAsync(Guid id, MCQ mCQ)
        {
            var existingMCQ = await dbContext.MCQs.FirstOrDefaultAsync(x =>x.Id == id);

            if (existingMCQ == null) 
            { 
                return null;
            }

            existingMCQ.McqNumber = mCQ.McqNumber;
            existingMCQ.Question = mCQ.Question;
            existingMCQ.Option1 = mCQ.Option1;
            existingMCQ.Option2 = mCQ.Option2;
            existingMCQ.Option3 = mCQ.Option3;
            existingMCQ.Option4 = mCQ.Option4;
            existingMCQ.RightAnswer = mCQ.RightAnswer;

            await dbContext.SaveChangesAsync();
            return existingMCQ;
        }
    }
}
