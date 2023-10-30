using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assinatura",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoAssinatura = table.Column<int>(type: "int", nullable: false),
                    Preco = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: false),
                    TipoPlano = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assinatura", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Noticia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubTitulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Conteudo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lead = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Regiao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExclusivoParaAssinantes = table.Column<bool>(type: "bit", nullable: false),
                    Ativa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Noticia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RedesSociais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RedesSociais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Texto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: true),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "varchar(max)", nullable: true),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoriaNoticia",
                columns: table => new
                {
                    CategoriasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NoticiasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaNoticia", x => new { x.CategoriasId, x.NoticiasId });
                    table.ForeignKey(
                        name: "FK_CategoriaNoticia_Categorias_CategoriasId",
                        column: x => x.CategoriasId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoriaNoticia_Noticia_NoticiasId",
                        column: x => x.NoticiasId,
                        principalTable: "Noticia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NoticiaNoticia",
                columns: table => new
                {
                    NoticiaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NoticiasRelacionadasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoticiaNoticia", x => new { x.NoticiaId, x.NoticiasRelacionadasId });
                    table.ForeignKey(
                        name: "FK_NoticiaNoticia_Noticia_NoticiaId",
                        column: x => x.NoticiaId,
                        principalTable: "Noticia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NoticiaNoticia_Noticia_NoticiasRelacionadasId",
                        column: x => x.NoticiasRelacionadasId,
                        principalTable: "Noticia",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NoticiaTag",
                columns: table => new
                {
                    NoticiasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoticiaTag", x => new { x.NoticiasId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_NoticiaTag_Noticia_NoticiasId",
                        column: x => x.NoticiasId,
                        principalTable: "Noticia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NoticiaTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Administrador",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrador", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Administrador_Usuarios_Id",
                        column: x => x.Id,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assinante",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssinaturaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assinante", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assinante_Assinatura_AssinaturaId",
                        column: x => x.AssinaturaId,
                        principalTable: "Assinatura",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Assinante_Usuarios_Id",
                        column: x => x.Id,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Autor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Autor_Usuarios_Id",
                        column: x => x.Id,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comentario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Texto = table.Column<string>(type: "varchar(1000)", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    NoticiaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstadoValidacao = table.Column<int>(type: "int", nullable: false),
                    DataValidacao = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModeradorResponsavelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MotivoRejeicao = table.Column<string>(type: "varchar(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comentario_Administrador_ModeradorResponsavelId",
                        column: x => x.ModeradorResponsavelId,
                        principalTable: "Administrador",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comentario_Noticia_NoticiaId",
                        column: x => x.NoticiaId,
                        principalTable: "Noticia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comentario_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AutorNoticia",
                columns: table => new
                {
                    AutoresId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NoticiasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutorNoticia", x => new { x.AutoresId, x.NoticiasId });
                    table.ForeignKey(
                        name: "FK_AutorNoticia_Autor_AutoresId",
                        column: x => x.AutoresId,
                        principalTable: "Autor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AutorNoticia_Noticia_NoticiasId",
                        column: x => x.NoticiasId,
                        principalTable: "Noticia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AutorRedeSocial",
                columns: table => new
                {
                    AutoresId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RedesSociaisId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutorRedeSocial", x => new { x.AutoresId, x.RedesSociaisId });
                    table.ForeignKey(
                        name: "FK_AutorRedeSocial_Autor_AutoresId",
                        column: x => x.AutoresId,
                        principalTable: "Autor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AutorRedeSocial_RedesSociais_RedesSociaisId",
                        column: x => x.RedesSociaisId,
                        principalTable: "RedesSociais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assinante_AssinaturaId",
                table: "Assinante",
                column: "AssinaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_AutorNoticia_NoticiasId",
                table: "AutorNoticia",
                column: "NoticiasId");

            migrationBuilder.CreateIndex(
                name: "IX_AutorRedeSocial_RedesSociaisId",
                table: "AutorRedeSocial",
                column: "RedesSociaisId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriaNoticia_NoticiasId",
                table: "CategoriaNoticia",
                column: "NoticiasId");

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_ModeradorResponsavelId",
                table: "Comentario",
                column: "ModeradorResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_NoticiaId",
                table: "Comentario",
                column: "NoticiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_UsuarioId",
                table: "Comentario",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_NoticiaNoticia_NoticiasRelacionadasId",
                table: "NoticiaNoticia",
                column: "NoticiasRelacionadasId");

            migrationBuilder.CreateIndex(
                name: "IX_NoticiaTag_TagsId",
                table: "NoticiaTag",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assinante");

            migrationBuilder.DropTable(
                name: "AutorNoticia");

            migrationBuilder.DropTable(
                name: "AutorRedeSocial");

            migrationBuilder.DropTable(
                name: "CategoriaNoticia");

            migrationBuilder.DropTable(
                name: "Comentario");

            migrationBuilder.DropTable(
                name: "NoticiaNoticia");

            migrationBuilder.DropTable(
                name: "NoticiaTag");

            migrationBuilder.DropTable(
                name: "Assinatura");

            migrationBuilder.DropTable(
                name: "Autor");

            migrationBuilder.DropTable(
                name: "RedesSociais");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Administrador");

            migrationBuilder.DropTable(
                name: "Noticia");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
