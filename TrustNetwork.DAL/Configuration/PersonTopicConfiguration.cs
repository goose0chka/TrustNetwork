using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrustNetwork.DAL.Model;

namespace TrustNetwork.DAL.Configuration;

internal class PersonTopicConfiguration : IEntityTypeConfiguration<PersonTopic>
{
    public void Configure(EntityTypeBuilder<PersonTopic> builder)
    {
        builder.HasKey(x => new { x.PersonId, x.TopicId });
        builder.HasOne(x => x.Person).WithMany(x => x.PersonTopics).HasForeignKey(x => x.PersonId);
        builder.HasOne(x => x.Topic).WithMany(x => x.PersonTopics).HasForeignKey(x => x.TopicId);
    }
}
