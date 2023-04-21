using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntroduccionAEFCore.Migrations
{
    /// <inheritdoc />
    /// 

    //La migraciones son como un paso intermedio entre los cambios que hiciste en el programa y la aplicacion de estos cambios en la base de datos 

    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        /// 
        // Vacicamente es lo que hace la migracion cuando la aplicamos
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.Id);
                });
        }

        /// <inheritdoc />
        /// 

        // Este metodo sirve para remover una migracion
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Generos");
        }
    }
}
