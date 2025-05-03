using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Infrastructure.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "data");

            migrationBuilder.EnsureSchema(
                name: "kernel");

            migrationBuilder.CreateTable(
                name: "diseases",
                schema: "kernel",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    period_type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_diseases", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "patients",
                schema: "data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    fio = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    birthday = table.Column<DateOnly>(type: "date", nullable: false),
                    snils = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_patients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    login = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "contacts",
                schema: "data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    patient_id = table.Column<Guid>(type: "uuid", nullable: false),
                    disease_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contacts", x => x.id);
                    table.ForeignKey(
                        name: "fk_contacts_diseases_disease_id",
                        column: x => x.disease_id,
                        principalSchema: "kernel",
                        principalTable: "diseases",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_contacts_patients_patient_id",
                        column: x => x.patient_id,
                        principalSchema: "data",
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "resources_spent",
                schema: "data",
                columns: table => new
                {
                    resource = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    comment = table.Column<string>(type: "text", maxLength: 250, nullable: false),
                    contact_id = table.Column<Guid>(type: "uuid", nullable: false),
                    count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_resources_spent", x => new { x.contact_id, x.resource, x.comment });
                    table.ForeignKey(
                        name: "fk_resources_spent_contacts_contact_id",
                        column: x => x.contact_id,
                        principalSchema: "data",
                        principalTable: "contacts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_contacts_disease_id",
                schema: "data",
                table: "contacts",
                column: "disease_id");

            migrationBuilder.CreateIndex(
                name: "ix_contacts_patient_id_disease_id_date",
                schema: "data",
                table: "contacts",
                columns: new[] { "patient_id", "disease_id", "date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_diseases_name",
                schema: "kernel",
                table: "diseases",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_diseases_name_period_type",
                schema: "kernel",
                table: "diseases",
                columns: new[] { "name", "period_type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_diseases_period_type",
                schema: "kernel",
                table: "diseases",
                column: "period_type");

            migrationBuilder.CreateIndex(
                name: "ix_patients_fio",
                schema: "data",
                table: "patients",
                column: "fio");

            migrationBuilder.CreateIndex(
                name: "ix_patients_fio_birthday_snils",
                schema: "data",
                table: "patients",
                columns: new[] { "fio", "birthday", "snils" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_patients_snils",
                schema: "data",
                table: "patients",
                column: "snils",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_resources_spent_contact_id",
                schema: "data",
                table: "resources_spent",
                column: "contact_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_login",
                schema: "data",
                table: "users",
                column: "login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_login_password_hash",
                schema: "data",
                table: "users",
                columns: new[] { "login", "password_hash" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "resources_spent",
                schema: "data");

            migrationBuilder.DropTable(
                name: "users",
                schema: "data");

            migrationBuilder.DropTable(
                name: "contacts",
                schema: "data");

            migrationBuilder.DropTable(
                name: "diseases",
                schema: "kernel");

            migrationBuilder.DropTable(
                name: "patients",
                schema: "data");
        }
    }
}