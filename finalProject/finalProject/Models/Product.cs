using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finalProject.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("פריט")]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "גודל")]
        [Range(0, int.MaxValue)]
        public int Size { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        [DisplayName("מחיר")]
        public double Price { get; set; }

        [DisplayName("תמונה")]
        public string PictureName { get; set; }

        public virtual List<Purchase> Purchases { get; set; }

        public int? SupplierId { get; set; }

        [DisplayName("חברה")]
        public virtual Supplier Supplier { get; set; }

        public int? ProductTypeId { get; set; }

        [DisplayName("סוג מוצר")]
        public virtual ProductType ProductType { get; set; }
    }
}