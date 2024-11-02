using ManagementSystem.Domain.Entities;
using ManagementSystem.Domain.Models.Enums;
using ManagementSystem.Domain.TokenHandler;
using Microsoft.EntityFrameworkCore;
using Packages.Repositories.EfCore.Entity;
using Packages.Repositories.Enums;
using System.Reflection;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using StatusType = ManagementSystem.Domain.Models.Enums.StatusType;

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
        public DbSet<Project> Projects { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Quarter> Quarters { get; set; }
        public DbSet<StatusChangeLog> StatusChangeLogs { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connStr = "server=.\\;database=ProjectManagement; integrated security=true; TrustServerCertificate=true;";
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
                .HasKey(ur => ur.Id);

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

            modelBuilder.Entity<Address>()
                .HasOne(a => a.User)
                .WithMany(u => u.Addresses) // User entity'sinde Addresses koleksiyonunu ekleyin
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // District tablosuyla City tablosu arasındaki ilişkiyi belirtme
            modelBuilder.Entity<District>()
                .HasOne(d => d.City)
                .WithMany(c => c.Districts)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.NoAction);


            // Quarter tablosuyla District tablosu arasındaki ilişkiyi belirtme
            modelBuilder.Entity<Quarter>()
                .HasOne(q => q.District)
                .WithMany(d => d.Quarters)
                .HasForeignKey(q => q.DistrictId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Address>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Address>()
                .HasOne(a => a.City)
                .WithMany()
                .HasForeignKey(a => a.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Address>()
                .HasOne(a => a.District)
                .WithMany()
                .HasForeignKey(a => a.DistrictId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Address>()
                .HasOne(a => a.Quarter)
                .WithMany()
                .HasForeignKey(a => a.QuerterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Address>()
                .Property(a => a.Description)
                .HasMaxLength(500);


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
                    data.Entity.Status = StatusType.Pending.ToString();
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
            // ChangeTracker'dan liste oluştur
            var entries = ChangeTracker.Entries<BaseEntity>().ToList();
            var claims = _domainPrincipal.GetClaims(); // Kullanıcı bilgilerini al
            var statusChangeLogs = new List<StatusChangeLog>(); // Log kayıtları için liste

            foreach (var data in entries)
            {
                int recordId = data.Property("Id").CurrentValue != null ? (int)data.Property("Id").CurrentValue : 0; // ID'yi almak için kontrol et

                if (data.State == EntityState.Added)
                {
                    // Yeni kayıt için oluşturma işlemleri
                    data.Entity.CreatedOn = DateTime.Now;
                    data.Entity.CreatedBy = string.Concat(claims.Name, " ", claims.LastName);
                    data.Entity.CreatedById = claims.Id;

                    if (data.Entity.Status is null)
                    {
                        data.Entity.Status = StatusType.Pending.ToString(); // Varsayılan status
                    }

                    if (data.Entity is Comment comment)
                    {
                        if (comment.Status != StatusType.Published.ToString())
                        {
                            comment.Status = StatusType.Published.ToString(); // Durum değişikliği
                        }
                    }

                    // Log kaydı oluştur
                    var log = new StatusChangeLog
                    {
                        TableName = data.Metadata.GetTableName(),
                        RecordId = default,
                        OldStatus = null, // Yeni ekleniyor, eski durum yok
                        NewStatus = data.Entity.Status,
                        ChangedBy = JsonSerializer.Serialize(new ChangedByInfo
                        {
                            Id = claims.Id,
                            Name = claims.Name,
                            Surname = claims.LastName
                        }, new JsonSerializerOptions
                        {
                            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping // Özel karakterler için esnek kodlama
                        }),
                        ChangedDate = DateTime.Now,
                        Status = StatusType.New.ToString()
                    };
                    statusChangeLogs.Add(log); // Log kaydını listeye ekle
                }
                else if (data.State == EntityState.Modified)
                {
                    // Güncelleme işlemleri
                    data.Entity.ModifiedOn = DateTime.Now;
                    data.Entity.ModifiedBy = string.Concat(claims.Name, " ", claims.LastName);
                    data.Entity.ModifiedById = claims.Id;

                    // Status alanını kontrol et ve log ekle
                    if (data.Properties.Any(p => p.Metadata.Name == "Status" && p.IsModified))
                    {
                        var oldStatus = data.Property("Status").OriginalValue?.ToString();
                        var newStatus = data.Property("Status").CurrentValue?.ToString();

                        // Log kaydı oluştur
                        var log = new StatusChangeLog
                        {
                            TableName = data.Metadata.GetTableName(),
                            RecordId = (int)data.Property("Id").CurrentValue,
                            OldStatus = oldStatus,
                            NewStatus = newStatus,
                            ChangedBy = JsonSerializer.Serialize(new ChangedByInfo
                            {
                                Id = claims.Id,
                                Name = claims.Name,
                                Surname = claims.LastName
                            }, new JsonSerializerOptions
                            {
                                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping // Özel karakterler için esnek kodlama
                            }),
                            ChangedDate = DateTime.Now,
                            Status = StatusType.Published.ToString()
                        };
                        statusChangeLogs.Add(log); // Log kaydını listeye ekle
                    }
                }
            }

            // Log kayıtlarını veritabanına ekle
            if (statusChangeLogs.Any())
            {
                StatusChangeLogs.AddRange(statusChangeLogs); // Tüm logları ekle
            }

            // Veritabanına değişiklikleri kaydet
            return await base.SaveChangesAsync(cancellationToken); // Değişiklikleri kaydet
        }
    }
}

