using Microsoft.EntityFrameworkCore;

namespace CalculatorAPI.Data
{
    public class CalculationDbContext : DbContext
    {
        public DbSet<Variant> Variants { get; set; }
        public DbSet<Cathegory> Cathegories { get; set; }

        public CalculationDbContext(DbContextOptions<CalculationDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
