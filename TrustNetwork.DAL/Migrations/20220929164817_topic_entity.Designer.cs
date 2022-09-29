﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TrustNetwork.DAL;

#nullable disable

namespace TrustNetwork.DAL.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220929164817_topic_entity")]
    partial class topic_entity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TrustNetwork.DAL.Model.Person", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("TrustNetwork.DAL.Model.PersonTopic", b =>
                {
                    b.Property<string>("PersonId")
                        .HasColumnType("text");

                    b.Property<int>("TopicId")
                        .HasColumnType("integer");

                    b.HasKey("PersonId", "TopicId");

                    b.HasIndex("TopicId");

                    b.ToTable("PersonTopics");
                });

            modelBuilder.Entity("TrustNetwork.DAL.Model.Relation", b =>
                {
                    b.Property<string>("PersonId")
                        .HasColumnType("text");

                    b.Property<string>("ContactId")
                        .HasColumnType("text");

                    b.Property<int>("TrustLevel")
                        .HasColumnType("integer");

                    b.HasKey("PersonId", "ContactId");

                    b.HasIndex("ContactId");

                    b.ToTable("Relations");
                });

            modelBuilder.Entity("TrustNetwork.DAL.Model.Topic", b =>
                {
                    b.Property<int>("TopicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TopicId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("TopicId");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("TrustNetwork.DAL.Model.PersonTopic", b =>
                {
                    b.HasOne("TrustNetwork.DAL.Model.Person", "Person")
                        .WithMany("PersonTopics")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrustNetwork.DAL.Model.Topic", "Topic")
                        .WithMany("PersonTopics")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("TrustNetwork.DAL.Model.Relation", b =>
                {
                    b.HasOne("TrustNetwork.DAL.Model.Person", "Contact")
                        .WithMany()
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrustNetwork.DAL.Model.Person", "Person")
                        .WithMany("Relations")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contact");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("TrustNetwork.DAL.Model.Person", b =>
                {
                    b.Navigation("PersonTopics");

                    b.Navigation("Relations");
                });

            modelBuilder.Entity("TrustNetwork.DAL.Model.Topic", b =>
                {
                    b.Navigation("PersonTopics");
                });
#pragma warning restore 612, 618
        }
    }
}
