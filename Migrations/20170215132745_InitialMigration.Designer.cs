using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoApp.Migrations
{
    [DbContext(typeof(DemoContext))]
    [Migration("20170215132745_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1");

            modelBuilder.Entity("DemoApp.Models.Job", b =>
                {
                    b.Property<int>("JobId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AssignedToUserId");

                    b.Property<DateTime>("DueDate");

                    b.Property<bool>("IsComplete");

                    b.Property<string>("Title");

                    b.HasKey("JobId");

                    b.HasIndex("AssignedToUserId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("DemoApp.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DemoApp.Models.Job", b =>
                {
                    b.HasOne("DemoApp.Models.User", "AssignedTo")
                        .WithMany("Jobs")
                        .HasForeignKey("AssignedToUserId");
                });
        }
    }
}
