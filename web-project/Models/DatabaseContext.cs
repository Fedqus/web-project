using Microsoft.EntityFrameworkCore;

namespace web_project.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Passport> Passports { get; set; }
        public DbSet<DrivingLicence> DrivingLicences { get; set; }
        public DbSet<StudentCard> StudentCards { get; set; }
        public DbSet<News> News { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<DrivingLicence>()
                .HasKey(dl => dl.Id);

            modelBuilder.Entity<DrivingLicence>()
                .HasOne(dl => dl.User)
                .WithMany(u => u.DrivingLicences)
                .HasForeignKey(dl => dl.UserId);

            modelBuilder.Entity<StudentCard>()
                .HasKey(sc => sc.Id);

            modelBuilder.Entity<StudentCard>()
                .HasOne(sc => sc.User)
                .WithMany(u => u.StudentCards)
                .HasForeignKey(sc => sc.UserId);

            modelBuilder.Entity<Passport>()
                .HasKey(sc => sc.Id);

            modelBuilder.Entity<Passport>()
                .HasOne(sc => sc.User)
                .WithMany(u => u.Passports)
                .HasForeignKey(sc => sc.UserId);

            modelBuilder.Entity<News>()
                .HasKey(n => n.Id);
        }
    }

}
