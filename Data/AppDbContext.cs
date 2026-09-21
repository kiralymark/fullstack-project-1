using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace fullstack_project_1.Data
{

    //public class AppDbContext: DbContext
    public class AppDbContext : IdentityDbContext<AspNetUser>
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);  // with base.OnModelCreating, Identity configures default schemas

            builder.Entity<AspNetUser>(entity =>
            {
                entity.ToTable("AspNetUsers");
                
                // Explicitly map properties to PostgreSQL column names and data types, (Fluent API usage)
                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .HasColumnType("varchar(450)");

                entity.Property(e => e.AccessFailedCount)
                    .HasColumnName("AccessFailedCount");

                entity.Property(e => e.ConcurrencyStamp)
                    .HasColumnName("ConcurrencyStamp");

                entity.Property(e => e.Email)
                    .HasColumnName("Email")
                    .HasColumnType("varchar(256)");

                entity.Property(e => e.EmailConfirmed)
                    .HasColumnName("EmailConfirmed");

                entity.Property(e => e.LockoutEnabled)
                    .HasColumnName("LockoutEnabled");

                entity.Property(e => e.LockoutEnd)
                    .HasColumnName("LockoutEnd");

                entity.Property(e => e.NormalizedEmail)
                    .HasColumnName("NormalizedEmail")
                    .HasColumnType("varchar(256)");

                entity.Property(e => e.NormalizedUserName)
                    .HasColumnName("NormalizedUserName")
                    .HasColumnType("varchar(256)");

                entity.Property(e => e.PasswordHash)
                    .HasColumnName("PasswordHash");

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("PhoneNumber");

                entity.Property(e => e.PhoneNumberConfirmed)
                    .HasColumnName("PhoneNumberConfirmed");

                entity.Property(e => e.SecurityStamp)
                    .HasColumnName("SecurityStamp");

                entity.Property(e => e.TwoFactorEnabled)
                    .HasColumnName("TwoFactorEnabled");

                entity.Property(e => e.UserName)
                    .HasColumnName("UserName")
                    .HasColumnType("varchar(256)");
                
                });

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            // Inject configuration from ASP.NET Core DI container (reads environment variables correctly)
            string connectionString = Configuration.GetConnectionString("PostgresDb")
                ?? throw new InvalidOperationException("Connection string 'PostgresDb' not found.");

            optionsBuilder.UseNpgsql(connectionString);

        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

    }

}
