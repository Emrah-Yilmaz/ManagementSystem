using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ManagementSystem.Domain.Entities
{
    public class AppDbContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "dbo";

        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<WorkTask> Tasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=.\\;database=ProjectManagement; integrated security=true; TrustServerCertificate=true;");
            }
        }

        public override int SaveChanges()
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReference)
                {
                    switch (item.Entity)
                    {
                        case EntityState.Added:
                            {
                                entityReference.CreatedOn = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                entityReference.ModifiedOn = DateTime.Now;
                                break;
                            }
                    }
                }
            }

            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReference)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                entityReference.CreatedOn = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                Entry(entityReference).Property(x => x.CreatedOn).IsModified = false;

                                entityReference.ModifiedOn = DateTime.Now;
                                break;
                            }
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // User ve Role arasındaki ilişkiyi tanımlama
            modelBuilder.Entity<User>()
                .HasMany(u => u.WorkTasks) // Bir kullanıcının birden çok görevi olabilir
                .WithOne(t => t.User)  // Bir görevin bir kullanıcısı olabilir
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Görev tablosunda UserId dış anahtar olarak kullanılır

            modelBuilder.Entity<User>()
                .HasMany(u => u.Comments) // Bir kullanıcının birden çok yorumu olabilir
                .WithOne(c => c.User)     // Bir yorumun bir kullanıcısı olabilir
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);// Yorum tablosunda UserId dış anahtar olarak kullanılır

            modelBuilder.Entity<WorkTask>()
                .HasMany(t => t.Comments) // Bir görevin birden çok yorumu olabilir
                .WithOne(c => c.Task)     // Bir yorumun bir görevi olabilir
                .HasForeignKey(c => c.TaskId)
                .OnDelete(DeleteBehavior.Restrict); // Yorum tablosunda TaskId dış anahtar olarak kullanılır
        }
    }
}

