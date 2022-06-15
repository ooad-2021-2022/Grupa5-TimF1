using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F1.Models
  
{
    public class KupljeneKarte
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("RegistrovaniKorisnik")]
        public int IdKorisnika { get; set; }
        public RegistrovaniKorisnik Korisnik { get; set; }
        [ForeignKey("Karta")]
        public int IdKarte { get; set; }
        public Karta Karta { get; set; }
        public KupljeneKarte() { }
    }
}
