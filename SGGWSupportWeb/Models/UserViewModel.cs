﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SGGWSupportWeb.Models
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Adres email")]
        public string Email { get; set; }

        [Display(Name = "Numer telefonu")]
        public string PhoneNo { get; set; }

    }
}