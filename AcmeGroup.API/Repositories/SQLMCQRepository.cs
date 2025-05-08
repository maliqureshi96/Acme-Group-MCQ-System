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

        public async Task<List<MCQ>> GetAllAsync()
        {
            return await dbContext.MCQs.ToListAsync();
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
