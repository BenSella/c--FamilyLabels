using FamilyLabels.Objects;
using Microsoft.EntityFrameworkCore;

namespace FamilyLabels.Utils
{
    public class FamilyDbContext : DbContext
    {
        public FamilyDbContext(DbContextOptions<FamilyDbContext> options) : base(options) 
        {
            
        }

        public DbSet<FamilyTree> FamilyTrees { get; set; }
        public DbSet<MonthlyExpenses> MonthlyExpenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FamilyTree>()
                .HasMany(f => f.MonthlyExpenses)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
