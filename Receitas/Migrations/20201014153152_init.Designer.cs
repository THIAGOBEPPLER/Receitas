﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Receitas.Data;

namespace Receitas.Migrations
{
    [DbContext(typeof(ReceitaContext))]
    [Migration("20201014153152_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Receitas.Entities.Ingrediente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.ToTable("Ingredientes");
                });

            modelBuilder.Entity("Receitas.Entities.Receita", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Categoria");

                    b.Property<string>("Descricao");

                    b.Property<int>("Duracao");

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.ToTable("Receitas");
                });

            modelBuilder.Entity("Receitas.Entities.ReceitaIngrediente", b =>
                {
                    b.Property<int>("ReceitaId");

                    b.Property<int>("IngredienteId");

                    b.HasKey("ReceitaId", "IngredienteId");

                    b.HasIndex("IngredienteId");

                    b.ToTable("ReceitasIngredientes");
                });

            modelBuilder.Entity("Receitas.Entities.ReceitaIngrediente", b =>
                {
                    b.HasOne("Receitas.Entities.Ingrediente", "Ingrediente")
                        .WithMany()
                        .HasForeignKey("IngredienteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Receitas.Entities.Receita", "Receita")
                        .WithMany("ReceitaIngrediente")
                        .HasForeignKey("ReceitaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
