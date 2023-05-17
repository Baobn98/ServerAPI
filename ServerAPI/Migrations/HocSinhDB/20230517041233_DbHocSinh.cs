using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerAPI.Migrations.HocSinhDB
{
    public partial class DbHocSinh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lops",
                columns: table => new
                {
                    MaLop = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLop = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lops", x => x.MaLop);
                });

            migrationBuilder.CreateTable(
                name: "HocSinhs",
                columns: table => new
                {
                    MaHS = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenHS = table.Column<int>(type: "int", nullable: false),
                    MaLop = table.Column<int>(type: "int", nullable: false),
                    TuoiHS = table.Column<int>(type: "int", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LopMaLop = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocSinhs", x => x.MaHS);
                    table.ForeignKey(
                        name: "FK_HocSinhs_Lops_LopMaLop",
                        column: x => x.LopMaLop,
                        principalTable: "Lops",
                        principalColumn: "MaLop");
                });

            migrationBuilder.CreateTable(
                name: "MonHocs",
                columns: table => new
                {
                    MaMH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenMH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiemSo = table.Column<double>(type: "float", nullable: false),
                    HocSinhMaHS = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonHocs", x => x.MaMH);
                    table.ForeignKey(
                        name: "FK_MonHocs_HocSinhs_HocSinhMaHS",
                        column: x => x.HocSinhMaHS,
                        principalTable: "HocSinhs",
                        principalColumn: "MaHS");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HocSinhs_LopMaLop",
                table: "HocSinhs",
                column: "LopMaLop");

            migrationBuilder.CreateIndex(
                name: "IX_MonHocs_HocSinhMaHS",
                table: "MonHocs",
                column: "HocSinhMaHS");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonHocs");

            migrationBuilder.DropTable(
                name: "HocSinhs");

            migrationBuilder.DropTable(
                name: "Lops");
        }
    }
}
