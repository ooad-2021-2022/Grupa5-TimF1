using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F1.Models
{
    public class Karta
    {
        [Key]
        public int Id { get; set; }
        public int RedniBroj { get; set; }

        [EnumDataType(typeof(KategorijaSjedista))]
        public KategorijaSjedista Kategorija { get; set; }
        public double Cijena { get; set; }
        [ForeignKey("Utrka")]
        public int IdUtrke { get; set; }
        public Utrka Utrka { get; set; }
        public Karta() { }
    }
}
