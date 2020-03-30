using finalProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace finalProject
{
    public class User
    {
        [Key]
        [Display(Name = "מזהה")]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "שם משתמש")]
        public string Username { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "סיסמא")]
        public string Password { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "כתובת מגורים")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "מין")]
        public Gender Gender { get; set; }

        [Required]
        [Display(Name = "תאריך לידה")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "האם מנהל")]
        public Boolean IsAdmin { get; set; }

        public virtual List<Purchase> Purchases { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}