using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OrganicMarket.Core.Models;
using System.Threading.Tasks;

#nullable disable

namespace OrganicMarket.Data
{
    public partial class OrganicMarketContext : DbContext
    {
        public OrganicMarketContext()
        {
        }

        public OrganicMarketContext(DbContextOptions<OrganicMarketContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        private IDbContextTransaction _transaction;

        public void BeginTransaction()
        {
            _transaction = Database.BeginTransaction();
        }



        public async Task Commit()
        {
            try
            {
                await SaveChangesAsync();
                _transaction.Commit();
            }
            finally
            {
                _transaction.Dispose();
            }
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-30M9UIR\\LOCALHOST;User ID=DESKTOP-30M9UIR\\RDC Partner;Database=OrganicMarket;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_CI_AS");

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => new { e.NickName, e.Email }, "UQ__Users__F7BDD6A2D3DFFD42")
                    .IsUnique();

                //entity.Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

                entity.Property(e => e.Id).UseIdentityColumn();

                entity.Property(e => e.Autority)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NickName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
