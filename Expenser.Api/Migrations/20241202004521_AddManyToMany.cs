using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expenser.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransactionInProject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<int>(type: "int", nullable: false),
                    BudgetCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionInProject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionInProject_BudgetCategories_BudgetCategoryId",
                        column: x => x.BudgetCategoryId,
                        principalTable: "BudgetCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransactionInProject_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransactionInProject_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionInProject_BudgetCategoryId",
                table: "TransactionInProject",
                column: "BudgetCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionInProject_ProjectId",
                table: "TransactionInProject",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionInProject_TransactionId",
                table: "TransactionInProject",
                column: "TransactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionInProject");
        }
    }
}
