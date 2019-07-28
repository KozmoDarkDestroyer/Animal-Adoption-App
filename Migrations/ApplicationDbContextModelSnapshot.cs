﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using animal_adoption.context;

namespace animal_adoption.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("animal_adoption.Models.Adopter", b =>
                {
                    b.Property<int>("id_adopter")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("address")
                        .IsRequired()
                        .HasMaxLength(145);

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<string>("identification")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(45);

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasMaxLength(45);

                    b.HasKey("id_adopter");

                    b.HasIndex("email")
                        .IsUnique();

                    b.HasIndex("identification")
                        .IsUnique();

                    b.ToTable("Adopter");
                });

            modelBuilder.Entity("animal_adoption.Models.Form", b =>
                {
                    b.Property<int>("id_form")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("adotion_form")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<int>("id_adopter");

                    b.HasKey("id_form");

                    b.HasIndex("id_adopter");

                    b.ToTable("Form");
                });

            modelBuilder.Entity("animal_adoption.Models.Foundation", b =>
                {
                    b.Property<int>("id_foundation")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("address")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<string>("association")
                        .IsRequired()
                        .HasMaxLength(145);

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(75);

                    b.Property<string>("web")
                        .IsRequired()
                        .HasMaxLength(145);

                    b.HasKey("id_foundation");

                    b.HasIndex("email")
                        .IsUnique();

                    b.ToTable("Foundation");
                });

            modelBuilder.Entity("animal_adoption.Models.Pet", b =>
                {
                    b.Property<int>("id_pet")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("age");

                    b.Property<int>("id_adopter");

                    b.Property<int>("id_foundation");

                    b.Property<string>("img")
                        .HasMaxLength(500);

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(45);

                    b.Property<string>("race")
                        .IsRequired()
                        .HasMaxLength(45);

                    b.Property<string>("sex")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<string>("species")
                        .IsRequired()
                        .HasMaxLength(6);

                    b.HasKey("id_pet");

                    b.HasIndex("id_adopter");

                    b.HasIndex("id_foundation");

                    b.ToTable("Pet");
                });

            modelBuilder.Entity("animal_adoption.Models.User", b =>
                {
                    b.Property<int>("id_user")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<string>("img");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<string>("role")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<bool>("status");

                    b.HasKey("id_user");

                    b.HasIndex("email")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("animal_adoption.Models.Form", b =>
                {
                    b.HasOne("animal_adoption.Models.Adopter", "Adopter")
                        .WithMany("Forms")
                        .HasForeignKey("id_adopter")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("animal_adoption.Models.Pet", b =>
                {
                    b.HasOne("animal_adoption.Models.Adopter", "Adopter")
                        .WithMany("Pets")
                        .HasForeignKey("id_adopter")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("animal_adoption.Models.Foundation", "Foundation")
                        .WithMany("Pets")
                        .HasForeignKey("id_foundation")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
