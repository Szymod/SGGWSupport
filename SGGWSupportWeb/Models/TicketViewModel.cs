using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SGGWSupportWeb.Models
{
    public class TicketViewModel
    {
        [Display(Name = "Numer")]
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tytuł")]
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Status")]
        [JsonProperty(PropertyName = "status")]
        public StateViewModel Status { get; set; }

        [Required]
        [Display(Name = "Priorytet")]
        [JsonProperty(PropertyName = "priority")]
        public PriorityViewModel Priority { get; set; }

        [Required]
        [Display(Name = "Kategoria")]
        [JsonProperty(PropertyName = "category")]
        public CategoryViewModel Category { get; set; }

        [Required]
        [Display(Name = "Opis")]
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Data zgłoszenia")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [JsonProperty(PropertyName = "date")]
        [JsonConverter(typeof(MicrosecondEpochConverter))]
        public DateTime TicketAddTime { get; set; }

        [JsonProperty(PropertyName = "userData")]
        public CurrentUser UserData { get; set; }

        [JsonProperty(PropertyName = "comments")]
        public List<object> Comments { get; set; }
    }


    public class MicrosecondEpochConverter : IsoDateTimeConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null || ((long)reader.Value) < 0) { return DateTime.MinValue; }
            return (new DateTime(1970, 1, 1)).AddMilliseconds((long)reader.Value);
        }
    }
}