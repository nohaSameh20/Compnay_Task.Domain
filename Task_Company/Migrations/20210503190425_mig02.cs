using Microsoft.EntityFrameworkCore.Migrations;

namespace Company_Task.Migrations
{
    public partial class mig02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Voltage",
                table: "Consumptions",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "Current",
                table: "Consumptions",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Voltage",
                table: "Consumptions",
                type: "int",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "Current",
                table: "Consumptions",
                type: "int",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
