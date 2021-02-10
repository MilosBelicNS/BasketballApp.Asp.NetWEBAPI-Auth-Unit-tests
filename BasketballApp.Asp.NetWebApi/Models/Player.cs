using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasketballApp.Asp.NetWebApi.Models
{
    public class Player
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [Range(1975, 2003)]
        public int Born { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int Matches { get; set; }

        [Required]
        [Range(0, 29)]
        public decimal PointsAverage { get; set; }

        public int ClubId { get; set; }
        public Club Club { get; set; }
    }
}