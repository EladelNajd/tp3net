using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestoManager_X.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultSpecialite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TAvis",
                schema: "resto",
                columns: table => new
                {
                    CodeAvis = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomPersonne = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<int>(type: "int", nullable: false),
                    Commentaire = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NumResto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAvis", x => x.CodeAvis);
                    table.CheckConstraint("CK_Avis_Note", "[Note] BETWEEN 1 AND 5");
                    table.ForeignKey(
                        name: "Relation_Resto_Avis",
                        column: x => x.NumResto,
                        principalSchema: "resto",
                        principalTable: "TRestaurant",
                        principalColumn: "CodeResto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TAvis_NumResto",
                schema: "resto",
                table: "TAvis",
                column: "NumResto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TAvis",
                schema: "resto");
        }
    }
}
