using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrustNetwork.DAL.Model;

namespace TrustNetwork.DAL.Configuration;
class RelationConfiguration : IEntityTypeConfiguration<Relation>
{
    public void Configure(EntityTypeBuilder<Relation> builder)
    {
        builder.HasKey(rel => new { rel.PersonId, rel.ContactId });
        builder.HasOne(x => x.Person).WithMany().HasForeignKey(rel => rel.PersonId);
        builder.HasOne(x => x.Contact).WithMany().HasForeignKey(rel => rel.ContactId);
    }
}

