using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace K207Shopping.Web.Models
{
    public class Slider
    {
        public int ID { get; set; }
        public string PhotoUrl { get; set; }
        [MaxLength(80)]
        public string SubHeader { get; set; }
        [MaxLength(150)]
        public string Header { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }

    }
}
