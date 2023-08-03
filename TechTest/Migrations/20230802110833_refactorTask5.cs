using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechTest.Migrations
{
    /// <inheritdoc />
    public partial class refactorTask5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingList",
                table: "ShoppingList");

            migrationBuilder.RenameTable(
                name: "ShoppingList",
                newName: "ShoppingListItem");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingListItem",
                table: "ShoppingListItem",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingListItem",
                table: "ShoppingListItem");

            migrationBuilder.RenameTable(
                name: "ShoppingListItem",
                newName: "ShoppingList");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingList",
                table: "ShoppingList",
                column: "Id");
        }
    }
}
