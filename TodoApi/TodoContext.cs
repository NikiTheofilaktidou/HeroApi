using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi
{
    public class TodoContext :DbContext
    {

        protected readonly IConfiguration Configuration;

        public TodoContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("TodoApiDatabase"));
        }

        public DbSet<TodoItem> TodoItems { get; set; } = null!;
    }
}
