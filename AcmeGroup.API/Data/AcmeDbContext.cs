using AcmeGroup.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AcmeGroup.API.Data
{
    public class AcmeDbContext : DbContext
    {
        

        public AcmeDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            
        }

        public DbSet<MCQ> MCQs { get; set; }

    }
}
