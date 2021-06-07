﻿// <auto-generated />
using System;
using BomTratoData.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BomTratoData.Migrations
{
    [DbContext(typeof(BomtratoContext))]
    [Migration("20210528234837_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BomTratoDomain.Entities.Aprovador", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<DateTime>("BirthDate")
                        .HasMaxLength(100)
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Aprovador");
                });

            modelBuilder.Entity("BomTratoDomain.Entities.Escritorio", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Cep")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("District")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Number")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("State")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Street")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Escritorios");
                });

            modelBuilder.Entity("BomTratoDomain.Entities.Processo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AprovadorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Aproved")
                        .HasColumnType("bit");

                    b.Property<string>("ComplainerName")
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("EscritorioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProcessNumber")
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(12,2)");

                    b.HasKey("Id");

                    b.HasIndex("AprovadorId");

                    b.HasIndex("EscritorioId");

                    b.ToTable("Processos");
                });

            modelBuilder.Entity("BomTratoDomain.Entities.Processo", b =>
                {
                    b.HasOne("BomTratoDomain.Entities.Aprovador", "Aprovador")
                        .WithMany("Processos")
                        .HasForeignKey("AprovadorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BomTratoDomain.Entities.Escritorio", "Escritorio")
                        .WithMany("Processos")
                        .HasForeignKey("EscritorioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Aprovador");

                    b.Navigation("Escritorio");
                });

            modelBuilder.Entity("BomTratoDomain.Entities.Aprovador", b =>
                {
                    b.Navigation("Processos");
                });

            modelBuilder.Entity("BomTratoDomain.Entities.Escritorio", b =>
                {
                    b.Navigation("Processos");
                });
#pragma warning restore 612, 618
        }
    }
}
