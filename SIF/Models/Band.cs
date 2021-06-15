using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIF.Models
{
    public class Band
    {
        [Key]
        public int BandId { get; set; }

        [Display(Name = "Nombre")]
        public string BandName { get; set; }
    }
}
