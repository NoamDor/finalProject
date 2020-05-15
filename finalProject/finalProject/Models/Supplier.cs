using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace finalProject.Models
{
    public class Supplier
    {
        [Key]
        [DisplayName("מזהה")]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        [DisplayName("שם")]
        public string Name { get; set; }

        [StringLength(30)]
        [DisplayName("תמונה")]
        public string PictureName { get; set; }

        [DisplayName("רשימת מוצרים")]
        public virtual List<Product> Products { get; set; }
    }
}