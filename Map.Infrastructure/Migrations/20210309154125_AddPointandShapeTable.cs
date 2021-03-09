using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Map.Infrastructure.Migrations
{
    public partial class AddPointandShapeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shapes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FillColorCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FillOpacity = table.Column<double>(type: "float", nullable: false),
                    StrokeWeight = table.Column<int>(type: "int", nullable: false),
                    StrokeColorCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Geodesic = table.Column<bool>(type: "bit", nullable: false),
                    ZoomRatio = table.Column<int>(type: "int", nullable: false),
                    Radius = table.Column<float>(type: "real", nullable: true),
                    ShapeType = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shapes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Points",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lat = table.Column<double>(type: "float", nullable: false),
                    Lng = table.Column<double>(type: "float", nullable: false),
                    ShapeId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Points", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Points_Shapes_ShapeId",
                        column: x => x.ShapeId,
                        principalTable: "Shapes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Points_ShapeId",
                table: "Points",
                column: "ShapeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Points");

            migrationBuilder.DropTable(
                name: "Shapes");
        }
    }
}
