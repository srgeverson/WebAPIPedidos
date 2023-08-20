﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAPIPedidos.Domain.DAO.Repository;

#nullable disable

namespace WebAPIPedidos.Migrations
{
    [DbContext(typeof(ContextRepository))]
    [Migration("20230817193754_v3")]
    partial class v3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebAPIPedidos.Domain.Model.Entity.FornecedorEntity", b =>
                {
                    b.Property<long?>("Cnpj")
                        .HasColumnType("bigint");

                    b.Property<string>("EmailContato")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeContato")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RazaoSocial")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Uf")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Cnpj");

                    b.ToTable("fornecedores");
                });

            modelBuilder.Entity("WebAPIPedidos.Domain.Model.Entity.PedidoEntity", b =>
                {
                    b.Property<int?>("CodigoPedido")
                        .HasColumnType("int");

                    b.Property<long?>("Fornecedor")
                        .HasColumnType("bigint");

                    b.Property<int?>("Produto")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataPedido")
                        .HasColumnType("datetime2");

                    b.Property<int?>("QuantidadeProduto")
                        .HasColumnType("int");

                    b.Property<decimal?>("ValorPedido")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("CodigoPedido", "Fornecedor", "Produto");

                    b.ToTable("pedidos");
                });

            modelBuilder.Entity("WebAPIPedidos.Domain.Model.Entity.ProdutoEntity", b =>
                {
                    b.Property<int?>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Codigo"));

                    b.Property<DateTime?>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Codigo");

                    b.ToTable("produtos");
                });

            modelBuilder.Entity("WebAPIPedidos.Domain.Model.Entity.UsuarioEntity", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("Id"));

                    b.Property<DateTime?>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Login")
                        .IsUnique()
                        .HasFilter("[Login] IS NOT NULL");

                    b.ToTable("usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
