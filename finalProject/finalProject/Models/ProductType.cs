using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace finalProject.Models
{
    public class ProductType
    {
        [Key]
        [Display(Name = "מזהה")]
        public int Id { get; set; }

        [StringLength(30)]
        [Display(Name = "סוג מוצר")]
        public string Name { get; set; }

        [Display(Name = "מוצרים")]
        public virtual List<Product> Products { get; set; }
    }
}