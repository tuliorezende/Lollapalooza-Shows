using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lollapalooza.Services.Model
{
    public class UserSchedule
    {
        [Key, Column(Order = 0)]
        public string UserIdentifier { get; set; }
        [Key, ForeignKey("ShowId"), Column(Order = 1)]
        public int ShowId { get; set; }
        public virtual Show Show { get; set; }
    }
}
