using finalProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static finalProject.Enums.Enums;

namespace finalProject.Models
{
    public class User
    {
        [Key]
        [DisplayName("מזהה")]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        [DisplayName("שם משתמש")]
        public string Username { get; set; }

        [Required]
        [StringLength(30)]
        [DisplayName("סיסמא")]
        public string Password { get; set; }

        [Required]
        [StringLength(30)]
        [DisplayName("כתובת מגורים")]
        public string Address { get; set; }

        [Required]
        [DisplayName("מין")]
        public Gender Gender { get; set; }

        [Required]
        [DisplayName("תאריך לידה")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "המספר שהזנת אינו תקין")]
        [DisplayName("טלפון")]
        public string Telephone { get; set; }

        [Required]
        [DisplayName("האם מנהל")]
        public Boolean IsAdmin { get; set; }

        public virtual List<Purchase> Purchases { get; set; }
    }
}