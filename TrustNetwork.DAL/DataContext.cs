using Microsoft.EntityFrameworkCore;
using TrustNetwork.DAL.Model;

namespace TrustNetwork.DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) 
            : base(options) { }

        public DbSet<Person> Persons { get; set; } = null!;
        public DbSet<Relation> Relations { get; set; } = null!;
    }
}
