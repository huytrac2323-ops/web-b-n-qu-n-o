using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Demo.Migrations
{
    /// <inheritdoc />
    public partial class AddProductColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Reviews",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "Image", "Name", "Price", "Rating", "Reviews" },
                values: new object[,]
                {
                    { 1, "Flash Sale", "Áo phông trắng cơ bản, thoải mái và dễ kết hợp", "https://via.placeholder.com/200/ffffff/000000?text=Áo+Phông+Trắng", "Áo Phông Trắng", 199000.0, 4.7999999999999998, 24 },
                    { 2, "Flash Sale", "Quần shorts jean mát mẻ, phù hợp cho mùa hè", "https://via.placeholder.com/200/0066cc/ffffff?text=Quần+Shorts", "Quần Shorts Jean", 299000.0, 4.9000000000000004, 18 },
                    { 3, "Flash Sale", "Áo khoác denim bền bỉ và thời trang", "https://via.placeholder.com/200/663300/ffffff?text=Áo+Khoác", "Áo Khoác Denim", 499000.0, 4.7000000000000002, 32 },
                    { 4, "Flash Sale", "Áo crop top xanh nước biển, trẻ trung và gợi cầm", "https://via.placeholder.com/200/0099ff/ffffff?text=Crop+Top", "Áo Crop Top Xanh", 249000.0, 5.0, 15 },
                    { 5, "Trend Luxe", "Áo vest đen cao cấp, phù hợp cho các sự kiện chính thức", "https://via.placeholder.com/200/333333/ffffff?text=Áo+Vest", "Áo Vest Đen", 799000.0, 4.9000000000000004, 45 },
                    { 6, "Trend Luxe", "Váy trắng dự tiệc elegant, làm nổi bật vẻ đẹp", "https://via.placeholder.com/200/f0f0f0/000000?text=Váy+Trắng", "Váy Trắng Dự Tiệc", 899000.0, 5.0, 38 },
                    { 7, "Trend Luxe", "Áo sơ mi lụa mịn, có phần mềm mại và thoáng khí", "https://via.placeholder.com/200/cccccc/666666?text=Áo+Sơ+Mi", "Áo Sơ Mi Lụa", 699000.0, 4.7999999999999998, 28 },
                    { 8, "Trend Luxe", "Quần tây xám đơn giản, dễ kết hợp với nhiều áo", "https://via.placeholder.com/200/808080/ffffff?text=Quần+Tây", "Quần Tây Xám", 599000.0, 4.7000000000000002, 22 },
                    { 9, "Hot Trend", "Áo len đen ấm áp, thích hợp cho những ngày lạnh", "https://via.placeholder.com/200/1a1a1a/ffffff?text=Áo+Len", "Áo Len Đen", 349000.0, 4.9000000000000004, 51 },
                    { 10, "Hot Trend", "Váy đen midi kinh điển, phù hợp múi lạnh hàng ngày", "https://via.placeholder.com/200/2d2d2d/ffffff?text=Váy+Midi", "Váy Đen Midi", 459000.0, 4.7999999999999998, 42 },
                    { 11, "Hot Trend", "Áo trắng nữ đơn giản nhưng sang trọng", "https://via.placeholder.com/200/ffffff/000000?text=Áo+Trắng", "Áo Trắng Nữ", 249000.0, 5.0, 67 },
                    { 12, "Hot Trend", "Áo đỏ dự tiệc nổi bật, tự tin trong các dịp đặc biệt", "https://via.placeholder.com/200/cc0000/ffffff?text=Áo+Đỏ", "Áo Đỏ Dự Tiệc", 549000.0, 4.9000000000000004, 38 },
                    { 13, "Thời Trang Nam", "Quần jeans nam bền bỉ, dễ mix and match", "https://via.placeholder.com/200/003d7a/ffffff?text=Quần+Jeans", "Quần Jeans Nam", 379000.0, 4.7999999999999998, 55 },
                    { 14, "Thời Trang Nam", "Áo sơ mi nam chất lượng, thích hợp cho công sở", "https://via.placeholder.com/200/ffffff/000000?text=Áo+Sơ+Mi", "Áo Sơ Mi Nam", 299000.0, 4.7000000000000002, 33 },
                    { 15, "Thời Trang Nam", "Áo khoác nam ấm áp và thời trang", "https://via.placeholder.com/200/4d4d4d/ffffff?text=Áo+Khoác", "Áo Khoác Nam", 599000.0, 4.9000000000000004, 48 },
                    { 16, "Thời Trang Nam", "Quần tây nam sang trọng, phù hợp cho các dịp quan trọng", "https://via.placeholder.com/200/333333/ffffff?text=Quần+Tây", "Quần Tây Nam", 449000.0, 4.7999999999999998, 29 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Reviews",
                table: "Products");
        }
    }
}
