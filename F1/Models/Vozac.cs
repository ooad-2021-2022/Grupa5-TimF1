using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F1.Models
{
    public class Vozac
    {
        [Key]
        [Display(Name = "Vozač")]
        public int Id { get; set; }

        public string Ime { get; set; }
        public string Prezime { get; set; }
        [Display(Name = "Država")]

        public string Drzava { get; set; }

        public int Bodovi { get; set; }
        [ForeignKey("Ekipa")]
        public int IdEkipe { get; set; }
        public Ekipa Ekipa { get; set; }

        public Vozac() { }

        public override string ToString()
        {
            return Ime + " " + Prezime;
        }
    }
}
