using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static finalProject.Enums.Enums;

namespace finalProject.Models
{
    public class ProductType
    {
        [Key]
        [DisplayName("מזהה")]
        public int Id { get; set; }

        [StringLength(30)]
        [DisplayName("סוג מוצר")]
        public string Name { get; set; }

        [Display(Name = "מין")]
        public Gender Gender { get; set; }

        [DisplayName("מוצרים")]
        public virtual List<Product> Products { get; set; }
    }
}