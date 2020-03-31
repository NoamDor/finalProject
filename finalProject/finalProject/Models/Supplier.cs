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
        [DisplayName("שם ספק")]
        public string Name { get; set; }

        [Required]
        [StringLength(30)]
        [DisplayName("כתובת")]
        public string Address { get; set; }

        [Required]
        [DisplayName("קו רוחב")]
        public double Lat { get; set; }

        [Required]
        [DisplayName("קו אורך")]
        public double Long { get; set; }

        [DisplayName("רשימת מוצרים")]
        public virtual List<Product> Products { get; set; }
    }
}