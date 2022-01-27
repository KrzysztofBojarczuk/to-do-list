using Microsoft.EntityFrameworkCore;
using todoWebApi.Models;

namespace todoWebApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Todo> Todos { get; set; }
    }
}
