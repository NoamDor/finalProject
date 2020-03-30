using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace finalProject.Models
{
    public class Supplier
    {
        [Key]
        [Display(Name = "מזהה")]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "שם ספק")]
        public string Name { get; set; }

        [Display(Name = "רשימת מוצרים")]
        public virtual List<Product> Products { get; set; }
    }
}