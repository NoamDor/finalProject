using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace finalProject.Models
{
    public class Product
    {
        [Key]
        [Display(Name = "מזהה")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "שם")]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        [Display(Name = "מחיר")]
        public double Price { get; set; }

        [Display(Name = "תמונה")]
        public string PictureName { get; set; }

        public virtual List<Purchase> Purchases { get; set; }

        public int? SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }

        public int? ProductTypeId { get; set; }

        [Display(Name = "סוג")]
        public virtual ProductType ProductType { get; set; }
    }
}