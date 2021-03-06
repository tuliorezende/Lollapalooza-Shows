﻿using Lollapalooza.Services.CustomFormatter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lollapalooza.Services.Model
{
    public class Show
    {
        [Key, JsonProperty("ShowId")]
        public int ShowId { get; set; }
        [Required, JsonProperty("Day")]
        public string Day { get; set; }
        [Required, JsonProperty("Band")]
        public string Band { get; set; }
        [Required, JsonProperty("StartTime")]
        public string StartTime { get; set; }
        [Required, JsonProperty("EndTime")]
        public string EndTime { get; set; }
        [Required, JsonProperty("Stage")]
        public string Stage { get; set; }
        [Required, JsonProperty("ImageUrl")]
        public string ImageUrl { get; set; }
        [Required, JsonProperty("ShowDate"), JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
        public DateTime ShowDate { get; set; }
        [JsonIgnore]
        public virtual List<UserSchedule> UserScheduler { get; set; }
    }
}
