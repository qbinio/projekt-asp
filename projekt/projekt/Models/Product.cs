using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projekt.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage ="Prosze podac dodatnia cena")]
        [Display(Name="Cena label")]
        public decimal Price { get; set; }
        [Display(Name = "Opis")]
        public string Description { get; set; }
        [Display(Name = "Kategoria")]
        public string Category { get; set; }
    }
}
