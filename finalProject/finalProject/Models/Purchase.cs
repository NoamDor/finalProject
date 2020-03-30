using System;
using System.ComponentModel.DataAnnotations;

namespace finalProject.Models
{
    public class Purchase
    {
        [Key]
        [Display(Name = "מזהה רכישה")]
        public int Id { get; set; }

        [Display(Name = "תאריך רכישה")]
        public DateTime Date { get; set; }

        public int? ProductId { get; set; }

        [Display(Name = "מוצר")]
        public virtual Product Product { get; set; }

        [Display(Name = "כמות")]
        public int Count { get; set; }

        public int? UserId { get; set; }

        [Display(Name = "משתמש")]
        public virtual User User { get; set; }
    }
}