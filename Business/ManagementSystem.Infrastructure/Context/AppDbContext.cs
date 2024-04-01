using ManagementSystem.Domain.Entities;
using ManagementSystem.Domain.Models.Enums;
using ManagementSystem.Domain.TokenHandler;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ManagementSystem.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "dbo";
        private readonly IDomainPrincipal _domainPrincipal;

        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options, IDomainPrincipal domainPrincipal) : base(options)
        {
            _domainPrincipal = domainPrincipal;
        }

        public DbSet<User> User { get; set; }
        public DbSet<WorkTask> Task { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Department> Department { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connStr = "server=.\\;database=ProjectManagement5; integrated security=true; TrustServerCertificate=true;";
                optionsBuilder.UseSqlServer(connStr, opt =>
                {
                    opt.EnableRetryOnFailure();
                });
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Task)
                .WithMany(t => t.Comments)
                .HasForeignKey(c => c.TaskId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Department)
                .WithMany(d => d.Users)
                .HasForeignKey(u => u.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<WorkTask>()
                .HasMany(wt => wt.Comments)
                .WithOne(c => c.Task)
                .HasForeignKey(c => c.TaskId);

            modelBuilder.Entity<WorkTask>()
                .HasOne(p => p.Project)
                .WithMany(wt => wt.WorkTasks)
                .HasForeignKey(p => p.ProjectId)
                .OnDelete(DeleteBehavior.NoAction);

            

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override int SaveChanges()
        {
            var datas = ChangeTracker.Entries<BaseEntity>();
            var claims = _domainPrincipal.GetClaims();
            foreach (var data in datas)
            {
                if (data.State == EntityState.Added)
                {
                    data.Entity.CreatedOn = DateTime.Now;
                    data.Entity.CreatedBy = string.Concat(claims.Name + " " + claims.LastName);
                    data.Entity.CreatedById = claims.Id;
                    data.Entity.Status = StatusEnum.Pending.ToString();
                }
                else if (data.State == EntityState.Modified)
                {
                    data.Entity.ModifiedOn = DateTime.Now;
                    data.Entity.ModifiedBy = string.Concat(claims.Name + " " + claims.LastName);
                    data.Entity.ModifiedById = claims.Id;
                }
            }

            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            var datas = ChangeTracker.Entries<BaseEntity>();
            var claims = _domainPrincipal.GetClaims();

            foreach (var data in datas)
            {
                if (data.State == EntityState.Added)
                {
                    data.Entity.CreatedOn = DateTime.Now;
                    data.Entity.CreatedBy = string.Concat(claims.Name + " " + claims.LastName);
                    data.Entity.CreatedById = claims.Id;
                    data.Entity.Status = StatusEnum.Pending.ToString();
                    if (data.Entity is Comment)
                    {
                        var comment = (Comment)data.Entity;
                        if (comment.Status != StatusEnum.Published.ToString())
                        {
                            comment.Status = StatusEnum.Published.ToString();
                        }
                    }
                }
                else if (data.State == EntityState.Modified)
                {
                    data.Entity.ModifiedOn = DateTime.Now;
                    data.Entity.ModifiedBy = string.Concat(claims.Name + " " + claims.LastName);
                    data.Entity.ModifiedById = claims.Id;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}

