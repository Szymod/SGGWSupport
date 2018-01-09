using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SGGWSupportWeb.Models
{
    public abstract class BaseDictionaryViewModel
    {

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [DisplayName("Nazwa")]
        [JsonProperty(PropertyName = "name")]
        [Required]
        public string Name { get; set; }
    }
}