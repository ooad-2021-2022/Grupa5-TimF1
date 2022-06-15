using F1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Vozac> vozaci {get; set;}
        public DbSet<Ekipa> ekipe { get; set; }
        public DbSet<Utrka> utrke { get; set; }
        public DbSet<RegistrovaniKorisnik> korisnici { get; set; }
        public DbSet<RezultatZaVozaca> rezultatUtrke{ get; set; }
        public DbSet<KupljeneKarte> kupljeneKarte { get; set; }
        public DbSet<BankovniRacun> racuni { get; set; }
        public DbSet<Karta> karte { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vozac>().ToTable("Vozac");
            modelBuilder.Entity<Ekipa>().ToTable("Ekipa");
            modelBuilder.Entity<Utrka>().ToTable("Utrka");
            modelBuilder.Entity<RegistrovaniKorisnik>().ToTable("RegistrovaniKorisnik");
            modelBuilder.Entity<RezultatZaVozaca>().ToTable("RezultatZaVozaca");
            modelBuilder.Entity<KupljeneKarte>().ToTable("KupljeneKarte");
            modelBuilder.Entity<BankovniRacun>().ToTable("BankovniRacun");
            modelBuilder.Entity<Karta>().ToTable("Karta");
            base.OnModelCreating(modelBuilder);
        }
    }
}
