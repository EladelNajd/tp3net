using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestoManager_X.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "resto");

            migrationBuilder.CreateTable(
                name: "TProprietaire",
                schema: "resto",
                columns: table => new
                {
                    Numéro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomProp = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EmailProp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GsmProp = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TProprietaire", x => x.Numéro);
                });

            migrationBuilder.CreateTable(
                name: "TRestaurant",
                schema: "resto",
                columns: table => new
                {
                    CodeResto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomResto = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SpecResto = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Tunisienne"),
                    VilleResto = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TelResto = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Numprop = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRestaurant", x => x.CodeResto);
                    table.ForeignKey(
                        name: "Relation_Proprio_Restos",
                        column: x => x.Numprop,
                        principalSchema: "resto",
                        principalTable: "TProprietaire",
                        principalColumn: "Numéro",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TRestaurant_Numprop",
                schema: "resto",
                table: "TRestaurant",
                column: "Numprop");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TRestaurant",
                schema: "resto");

            migrationBuilder.DropTable(
                name: "TProprietaire",
                schema: "resto");
        }
    }
}
