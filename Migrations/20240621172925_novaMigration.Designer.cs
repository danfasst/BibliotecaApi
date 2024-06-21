﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Biblioteca.Migrations
{
    [DbContext(typeof(BibliotecaContext))]
    [Migration("20240621172925_novaMigration")]
    partial class novaMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Editora", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Editoras");
                });

            modelBuilder.Entity("Emprestimo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("DataDevolucao")
                        .HasColumnType("longtext");

                    b.Property<string>("DataEmprestimo")
                        .HasColumnType("longtext");

                    b.Property<string>("Livro")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Emprestimos");
                });

            modelBuilder.Entity("Livro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AnoPublicado")
                        .HasColumnType("longtext");

                    b.Property<string>("Autor")
                        .HasColumnType("longtext");

                    b.Property<string>("Categoria")
                        .HasColumnType("longtext");

                    b.Property<int?>("EditoraId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("EditoraId");

                    b.ToTable("Livros");
                });

            modelBuilder.Entity("Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CPF")
                        .HasColumnType("longtext");

                    b.Property<string>("DataNascimento")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.Property<string>("Telefone")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Livro", b =>
                {
                    b.HasOne("Editora", "Editora")
                        .WithMany("Livros")
                        .HasForeignKey("EditoraId");

                    b.Navigation("Editora");
                });

            modelBuilder.Entity("Editora", b =>
                {
                    b.Navigation("Livros");
                });
#pragma warning restore 612, 618
        }
    }
}