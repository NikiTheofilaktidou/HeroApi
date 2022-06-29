using Microsoft.EntityFrameworkCore;
using HeroApi.Models;

namespace HeroApi
{
    public class HeroContext :DbContext
    {

        protected readonly IConfiguration Configuration;

        public HeroContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("HeroApiDatabase"));
        }

        public DbSet<HeroModel> Heroes { get; set; } = null!;
    }
}
