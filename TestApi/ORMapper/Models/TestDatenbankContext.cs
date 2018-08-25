using Microsoft.EntityFrameworkCore;

namespace TestApi.ORMapper.Models
{
    public partial class TestDatenbankContext : DbContext
    {
        public virtual DbSet<Book> Books { get; set; }

        public TestDatenbankContext(DbContextOptions<TestDatenbankContext> options): base(options)
        {
          
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.ArticleNumber);

                entity.Property(e => e.ArticleNumber)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
