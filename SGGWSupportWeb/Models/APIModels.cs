using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGGWSupportWeb.Models
{
    public class APIResponse
    {
        [JsonProperty(PropertyName = "errorCode")]
        public string ErrorCode { get; set; }

        [JsonProperty(PropertyName = "errorDesc")]
        public string ErrorDesc { get; set; }

        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }
    }
}