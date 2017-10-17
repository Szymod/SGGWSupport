using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SGGWSupportWeb.Models
{
    public class TicketViewModel
    {
        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        public TicketStatus Status { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Priorytet")]
        public string Priority { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Kategoria")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Data zgłoszenia")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime TicketAddTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ostatnia edycja")]
        public DateTime TicketEditTime { get; set; }


    }

    public enum TicketStatus
    {
        Przyjęte,
        [Display(Name = "W trakcie")]
        WTrakcie, 
        Wykonane
    };
}