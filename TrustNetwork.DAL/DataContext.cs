using Microsoft.EntityFrameworkCore;
using TrustNetwork.DAL.Configuration;
using TrustNetwork.DAL.Model;

namespace TrustNetwork.DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
            => modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonConfiguration).Assembly);

        public DbSet<Person> Persons { get; set; } = null!;
        public DbSet<Relation> Relations { get; set; } = null!;
        public DbSet<Topic> Topics { get; set; }
        public DbSet<PersonTopic> PersonTopics { get; set; }
    }
}
