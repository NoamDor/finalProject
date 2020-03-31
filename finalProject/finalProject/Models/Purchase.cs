﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace finalProject.Models
{
    public class Purchase
    {
        [Key]
        [DisplayName("מזהה רכישה")]
        public int Id { get; set; }

        [DisplayName("תאריך רכישה")]
        public DateTime Date { get; set; }

        public int? ProductId { get; set; }

        [DisplayName("מוצר")]
        public virtual Product Product { get; set; }

        [DisplayName("כמות")]
        public int Count { get; set; }

        public int? UserId { get; set; }

        [DisplayName("משתמש")]
        public virtual User User { get; set; }
    }
}