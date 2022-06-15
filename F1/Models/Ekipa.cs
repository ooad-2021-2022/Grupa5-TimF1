using System.ComponentModel.DataAnnotations;

namespace F1.Models
{
    public class Ekipa
    {
      
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int Bodovi { get; set; }
        public Ekipa() { }
        public override string ToString()
        {
            return Naziv;
        }
    }
}