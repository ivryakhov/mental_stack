using Microsoft.EntityFrameworkCore;

namespace mental_stack.Entities
{
    public class MemoryContext : DbContext
    {
        public DbSet<MStack> MStacks { get; set; }
        public MemoryContext(DbContextOptions<MemoryContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
