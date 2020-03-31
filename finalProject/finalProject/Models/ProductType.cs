using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

        [DisplayName("מוצרים")]
        public virtual List<Product> Products { get; set; }
    }
}