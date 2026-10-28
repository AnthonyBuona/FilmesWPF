using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FilmesWPF.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Filmes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Genero = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    Diretor = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    DuracaoMin = table.Column<int>(type: "int", nullable: false),
                    Nota = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filmes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Filmes",
                columns: new[] { "Id", "Ano", "Diretor", "DuracaoMin", "Genero", "Nota", "Titulo" },
                values: new object[,]
                {
                    { 1, 1972, "Francis Ford Coppola", 175, "Drama", 9.1999999999999993, "O Poderoso Chefão" },
                    { 2, 2014, "Christopher Nolan", 169, "Ficção Científica", 8.6999999999999993, "Interestelar" },
                    { 3, 1999, "Lana e Lilly Wachowski", 136, "Ação", 8.6999999999999993, "Matrix" },
                    { 4, 2001, "Peter Jackson", 178, "Fantasia", 8.9000000000000004, "O Senhor dos Anéis: A Sociedade do Anel" },
                    { 5, 2002, "Fernando Meirelles", 130, "Drama", 8.5999999999999996, "Cidade de Deus" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Nome", "Senha", "Username" },
                values: new object[] { 1, "Administrador", "123", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Filmes");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
