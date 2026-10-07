using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticasLDP3.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "alumnos",
                columns: table => new
                {
                    dni = table.Column<string>(type: "varchar(8)", nullable: false),
                    apellidonom = table.Column<string>(type: "varchar(50)", nullable: true),
                    provincia = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alumnos", x => x.dni);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    nombre = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.nombre);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "alumnos");

            migrationBuilder.DropTable(
                name: "usuarios");
        }
    }
}
