﻿// <auto-generated />
using Encord.ChannelService.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Encord.ChannelService.Migrations
{
    [DbContext(typeof(ChannelContext))]
    [Migration("20200409124624_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Encord.Common.Models.Channel", b =>
                {
                    b.Property<string>("GuildId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GuildID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("GuildId");

                    b.ToTable("Channels");
                });
#pragma warning restore 612, 618
        }
    }
}
