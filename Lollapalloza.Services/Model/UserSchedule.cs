using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lollapalooza.Services.Model
{
    public class UserSchedule
    {
        public string UserIdentifier { get; set; }
        [ForeignKey("ShowId")]
        public int ShowId { get; set; }
        public DateTime ScheduledDate { get; set; }
        [JsonIgnore]
        public virtual Show Show { get; set; }
    }
}
