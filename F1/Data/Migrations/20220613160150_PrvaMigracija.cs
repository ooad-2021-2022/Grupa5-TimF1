using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace F1.Data.Migrations
{
    public partial class PrvaMigracija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankovniRacun",
                columns: table => new
                {
                    BrojRacuna = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Iznos = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankovniRacun", x => x.BrojRacuna);
                });

            migrationBuilder.CreateTable(
                name: "Ekipa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bodovi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ekipa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegistrovaniKorisnik",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PremiumKod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdBankovniRacun = table.Column<int>(type: "int", nullable: false),
                    IdAsp = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrovaniKorisnik", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrovaniKorisnik_BankovniRacun_IdBankovniRacun",
                        column: x => x.IdBankovniRacun,
                        principalTable: "BankovniRacun",
                        principalColumn: "BrojRacuna",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vozac",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Drzava = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bodovi = table.Column<int>(type: "int", nullable: false),
                    IdEkipe = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vozac", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vozac_Ekipa_IdEkipe",
                        column: x => x.IdEkipe,
                        principalTable: "Ekipa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Utrka",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mjesto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BrojDostupnihKarata = table.Column<int>(type: "int", nullable: false),
                    IdVozaca = table.Column<int>(type: "int", nullable: false),
                    PobjednikId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utrka", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Utrka_Vozac_PobjednikId",
                        column: x => x.PobjednikId,
                        principalTable: "Vozac",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Karta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RedniBroj = table.Column<int>(type: "int", nullable: false),
                    Kategorija = table.Column<int>(type: "int", nullable: false),
                    Cijena = table.Column<double>(type: "float", nullable: false),
                    IdUtrke = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Karta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Karta_Utrka_IdUtrke",
                        column: x => x.IdUtrke,
                        principalTable: "Utrka",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RezultatZaVozaca",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdVozaca = table.Column<int>(type: "int", nullable: false),
                    Bodovi = table.Column<int>(type: "int", nullable: false),
                    IdUtrke = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RezultatZaVozaca", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RezultatZaVozaca_Utrka_IdUtrke",
                        column: x => x.IdUtrke,
                        principalTable: "Utrka",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RezultatZaVozaca_Vozac_IdVozaca",
                        column: x => x.IdVozaca,
                        principalTable: "Vozac",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KupljeneKarte",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdKorisnika = table.Column<int>(type: "int", nullable: false),
                    KorisnikId = table.Column<int>(type: "int", nullable: true),
                    IdKarte = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KupljeneKarte", x => x.ID);
                    table.ForeignKey(
                        name: "FK_KupljeneKarte_Karta_IdKarte",
                        column: x => x.IdKarte,
                        principalTable: "Karta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KupljeneKarte_RegistrovaniKorisnik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "RegistrovaniKorisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Karta_IdUtrke",
                table: "Karta",
                column: "IdUtrke");

            migrationBuilder.CreateIndex(
                name: "IX_KupljeneKarte_IdKarte",
                table: "KupljeneKarte",
                column: "IdKarte");

            migrationBuilder.CreateIndex(
                name: "IX_KupljeneKarte_KorisnikId",
                table: "KupljeneKarte",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrovaniKorisnik_IdBankovniRacun",
                table: "RegistrovaniKorisnik",
                column: "IdBankovniRacun");

            migrationBuilder.CreateIndex(
                name: "IX_RezultatZaVozaca_IdUtrke",
                table: "RezultatZaVozaca",
                column: "IdUtrke");

            migrationBuilder.CreateIndex(
                name: "IX_RezultatZaVozaca_IdVozaca",
                table: "RezultatZaVozaca",
                column: "IdVozaca");

            migrationBuilder.CreateIndex(
                name: "IX_Utrka_PobjednikId",
                table: "Utrka",
                column: "PobjednikId");

            migrationBuilder.CreateIndex(
                name: "IX_Vozac_IdEkipe",
                table: "Vozac",
                column: "IdEkipe");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KupljeneKarte");

            migrationBuilder.DropTable(
                name: "RezultatZaVozaca");

            migrationBuilder.DropTable(
                name: "Karta");

            migrationBuilder.DropTable(
                name: "RegistrovaniKorisnik");

            migrationBuilder.DropTable(
                name: "Utrka");

            migrationBuilder.DropTable(
                name: "BankovniRacun");

            migrationBuilder.DropTable(
                name: "Vozac");

            migrationBuilder.DropTable(
                name: "Ekipa");
        }
    }
}
