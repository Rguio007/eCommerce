using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace raycommerceproject.Migrations
{
    /// <inheritdoc />
    public partial class AddUserShoppingCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserShoppingCartId",
                table: "ShoppingCartItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserShoppingCarts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_UserShoppingCartId",
                table: "ShoppingCartItems",
                column: "UserShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_UserShoppingCarts_UserId",
                table: "UserShoppingCarts",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_UserShoppingCarts_UserShoppingCartId",
                table: "ShoppingCartItems",
                column: "UserShoppingCartId",
                principalTable: "UserShoppingCarts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_UserShoppingCarts_UserShoppingCartId",
                table: "ShoppingCartItems");

            migrationBuilder.DropTable(
                name: "UserShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartItems_UserShoppingCartId",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "UserShoppingCartId",
                table: "ShoppingCartItems");
        }
    }
}
