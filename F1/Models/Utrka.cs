using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F1.Models
{
    public class Utrka
    {
        public class ValidateDate : ValidationAttribute
        {
            protected override ValidationResult IsValid
            (object date, ValidationContext validationContext)
            {
                return (((DateTime)date).Year == 2022)
                ? ValidationResult.Success
                : new ValidationResult("Datumi utrka moraju biti u 2022. godini!");
            }
        }


        [Key]
        public int Id { get; set; }
        public string Mjesto { get; set; }
        public DateTime Datum { get; set; }
        [Display(Name = "Broj dostupnih karata")]
        public int BrojDostupnihKarata { get; set; }
        [ForeignKey("Vozac")]
        [Display(Name = "Pobjednik")]
        public int IdVozaca { get; set; }
        public Vozac Pobjednik { get; set; }
        public Utrka() { }
        public override string ToString()
        {
            return Mjesto;
        }
    }
}
