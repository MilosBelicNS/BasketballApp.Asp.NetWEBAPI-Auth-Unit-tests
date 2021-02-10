using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasketballApp.Asp.NetWebApi.Models
{
    public class Club
    {

        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength =2)]
        public string Name { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string League { get; set; }

        [Required]
        [Range(1945, 2000)]
        public int Founded { get; set; }

        [Required]
        [Range(0, 19)]
        public int Trophies { get; set; }
       
    }
}