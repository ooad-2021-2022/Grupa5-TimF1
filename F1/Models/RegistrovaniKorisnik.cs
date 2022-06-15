using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F1.Models
{
    public class RegistrovaniKorisnik
    {

        [Key]
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PremiumKod { get; set; }
        [ForeignKey("BankovniRacun")]
        public int IdBankovniRacun { get; set; }
        public BankovniRacun BankovniRacun { get; set; }

        [ForeignKey("AspNetUsers ")]
        public int IdAsp { get; set; }
        public RegistrovaniKorisnik() { }
    }

   
}

