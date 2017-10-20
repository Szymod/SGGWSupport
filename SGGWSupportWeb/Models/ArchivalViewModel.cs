using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SGGWSupportWeb.Models
{
    public class ArchivalViewModel
    {
        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Numer zgłoszenia")]
        public string IncidentNo { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Przypisano do")]
        public string AssignTo { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Status")]
        public string Status { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Kategoria")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Wnioskodawca")]
        public string Requestor { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Priorytet")]
        public string Priority { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Data wykonania")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime ResolvedDate { get; set; }
    }
}