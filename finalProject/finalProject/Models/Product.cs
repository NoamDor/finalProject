using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace finalProject.Models
{
    public class Product
    {
        [Key]
        [DisplayName("מזהה")]
        public int Id { get; set; }

        [Required]
        [DisplayName("שם")]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        [DisplayName("מחיר")]
        public double Price { get; set; }

        [Required]
        [Display(Name = "גודל")]
        [Range(0, int.MaxValue)]
        public int Size { get; set; }

        [DisplayName("תמונה")]
        public string PictureName { get; set; }

        public virtual List<Purchase> Purchases { get; set; }

        public int? SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }

        public int? ProductTypeId { get; set; }

        [DisplayName("סוג")]
        public virtual ProductType ProductType { get; set; }
    }
}