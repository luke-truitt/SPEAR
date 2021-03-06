﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SPEAR.Data;

namespace SPEAR.Migrations
{
    [DbContext(typeof(ResourceContext))]
    partial class ResourceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SPEAR.Data.Entities.Resource", b =>
                {
                    b.Property<Guid>("ResourceId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Available");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("Location");

                    b.Property<int>("ResourceType");

                    b.HasKey("ResourceId");

                    b.ToTable("Resources");
                });
#pragma warning restore 612, 618
        }
    }
}
