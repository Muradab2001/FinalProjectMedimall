using FinalProjectMedimall.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FinalProjectMedimall.DAL
{
    public class ApplicationDbContext: IdentityDbContext<AppUser>
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
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<WishListItem> WishListItems { get; set; }

        public DbSet<Discount> Discounts { get; set; }

        public DbSet<Comment> Comments { get; set; }





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
            modelBuilder.Entity<Category>()
           .HasIndex(c => c.Name)
           .IsUnique();
            base.OnModelCreating(modelBuilder);
        }
    }
}
