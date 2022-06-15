using System.ComponentModel.DataAnnotations;

namespace F1.Models
{
    public class BankovniRacun
    {
        [Key]
        public int BrojRacuna { get; set; }
        public double Iznos { get; set; }

        public BankovniRacun() { }
    }
}