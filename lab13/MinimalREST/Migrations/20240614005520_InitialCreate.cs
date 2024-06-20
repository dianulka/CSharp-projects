using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinimalREST.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Measurement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ApparatusId = table.Column<string>(type: "TEXT", nullable: false),
                    ApparatusVersion = table.Column<string>(type: "TEXT", nullable: true),
                    ApparatusSensorType = table.Column<string>(type: "TEXT", nullable: true),
                    ApparatusTubeType = table.Column<string>(type: "TEXT", nullable: true),
                    Temperature = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true),
                    HitsNumer = table.Column<string>(type: "TEXT", nullable: true),
                    CalibrationFunction = table.Column<string>(type: "TEXT", nullable: true),
                    StartTime = table.Column<string>(type: "TEXT", nullable: true),
                    EndTime = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurement", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Measurement");
        }
    }
}
