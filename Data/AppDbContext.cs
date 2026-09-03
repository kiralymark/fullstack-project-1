using Npgsql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace fullstack_project_1.Data
{

    public class AppDbContext: DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);

            //optionsBuilder.UseNpgsql(Configuration.)

            //try
            //{



            // Inject configuration from ASP.NET Core DI container (reads environment variables correctly)
            string connectionString = Configuration.GetConnectionString("PostgresDb")
                ?? throw new InvalidOperationException("Connection string 'PostgresDb' not found.");

            optionsBuilder.UseNpgsql(connectionString);


                //using var con = new NpgsqlConnection(connectionString);
                //con.OpenAsync();

            //}
            //catch (Exception ex)
            //{

            //Console.WriteLine($"Database connection error: {ex.Message}");
            //databaseDataString = $"Error loading data: {ex.Message}";

            //}

        }

        public DbSet<User> Users { get; set; }



    }

}