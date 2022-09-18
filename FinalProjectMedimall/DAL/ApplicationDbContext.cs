using FinalProjectMedimall.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FinalProjectMedimall.DAL
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {

        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Setting> Settings { get; set; }

        public DbSet<Medicine> Medicines { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<MedicineImage> MedicineImages { get; set; }
        public DbSet<Rate> Rates { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var item in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
                )
            {
                item.SetColumnType("decimal(6,2)");
            }
            modelBuilder.Entity<Setting>()
                        .HasIndex(s => s.Key)
                        .IsUnique();
            base.OnModelCreating(modelBuilder);
        }
    }
}
