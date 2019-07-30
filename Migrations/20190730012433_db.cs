using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace animal_adoption.Migrations
{
    public partial class db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Foundation",
                columns: table => new
                {
                    id_foundation = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(maxLength: 75, nullable: false),
                    address = table.Column<string>(maxLength: 150, nullable: false),
                    association = table.Column<string>(maxLength: 145, nullable: false),
                    email = table.Column<string>(maxLength: 150, nullable: false),
                    web = table.Column<string>(maxLength: 145, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foundation", x => x.id_foundation);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id_user = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    password = table.Column<string>(maxLength: 150, nullable: false),
                    role = table.Column<string>(maxLength: 15, nullable: false),
                    email = table.Column<string>(maxLength: 150, nullable: false),
                    status = table.Column<bool>(nullable: false),
                    img = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id_user);
                });

            migrationBuilder.CreateTable(
                name: "Pet",
                columns: table => new
                {
                    id_pet = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    species = table.Column<string>(maxLength: 6, nullable: false),
                    race = table.Column<string>(maxLength: 45, nullable: false),
                    age = table.Column<int>(nullable: false),
                    name = table.Column<string>(maxLength: 45, nullable: false),
                    sex = table.Column<string>(maxLength: 15, nullable: false),
                    img = table.Column<string>(maxLength: 500, nullable: true),
                    id_foundation = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pet", x => x.id_pet);
                    table.ForeignKey(
                        name: "FK_Pet_Foundation_id_foundation",
                        column: x => x.id_foundation,
                        principalTable: "Foundation",
                        principalColumn: "id_foundation",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Adopter",
                columns: table => new
                {
                    id_adopter = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(maxLength: 45, nullable: false),
                    identification = table.Column<string>(maxLength: 50, nullable: false),
                    phone = table.Column<string>(maxLength: 45, nullable: false),
                    email = table.Column<string>(maxLength: 150, nullable: false),
                    address = table.Column<string>(maxLength: 145, nullable: false),
                    id_pet = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adopter", x => x.id_adopter);
                    table.ForeignKey(
                        name: "FK_Adopter_Pet_id_pet",
                        column: x => x.id_pet,
                        principalTable: "Pet",
                        principalColumn: "id_pet",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Form",
                columns: table => new
                {
                    id_form = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    number_adults = table.Column<int>(nullable: false),
                    number_children = table.Column<int>(nullable: false),
                    age_children = table.Column<int>(maxLength: 50, nullable: false),
                    pet_race = table.Column<string>(maxLength: 50, nullable: false),
                    pets_before = table.Column<string>(maxLength: 75, nullable: false),
                    rason_adoption = table.Column<string>(maxLength: 150, nullable: false),
                    responsibility_pet = table.Column<string>(maxLength: 150, nullable: false),
                    pet_status_check = table.Column<bool>(nullable: false),
                    report = table.Column<string>(nullable: true),
                    id_adopter = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Form", x => x.id_form);
                    table.ForeignKey(
                        name: "FK_Form_Adopter_id_adopter",
                        column: x => x.id_adopter,
                        principalTable: "Adopter",
                        principalColumn: "id_adopter",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adopter_email",
                table: "Adopter",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Adopter_id_pet",
                table: "Adopter",
                column: "id_pet",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Adopter_identification",
                table: "Adopter",
                column: "identification",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Form_id_adopter",
                table: "Form",
                column: "id_adopter",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Foundation_email",
                table: "Foundation",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pet_id_foundation",
                table: "Pet",
                column: "id_foundation");

            migrationBuilder.CreateIndex(
                name: "IX_User_email",
                table: "User",
                column: "email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Form");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Adopter");

            migrationBuilder.DropTable(
                name: "Pet");

            migrationBuilder.DropTable(
                name: "Foundation");
        }
    }
}
