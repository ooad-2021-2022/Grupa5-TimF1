using System.ComponentModel.DataAnnotations;

namespace F1.Models
{
    public enum KategorijaSjedista
    {
       [Display(Name = "Start-cilj")]
        startCilj,
        [Display(Name = "Prvi sektor")]
        prviSektor,
        [Display(Name = "Drugi sektor")]
        drugiSektor,
        [Display(Name = "Treći sektor")]
        treciSektor
    }
}