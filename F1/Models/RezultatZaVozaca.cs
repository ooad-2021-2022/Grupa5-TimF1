using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F1.Models
{
    public class RezultatZaVozaca
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Vozac")]
        public int IdVozaca {get; set;}
        [Display(Name ="Vozač")]
        public Vozac Vozac { get; set; }
        [Required]
        [Range(0, 26, ErrorMessage = "Broj ostvarenih bodova mora biti između 0 i 26!")]
        [DisplayName("Broj bodova:")]
        public int Bodovi { get; set; }

        [ForeignKey("Utrka")]
        [DisplayName("ID utrke:")]
        public int IdUtrke { get; set; }
        public Utrka Utrka { get; set; }
        public RezultatZaVozaca() { }
    }
}
