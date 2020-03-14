using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Examen.Models
{
    public class Reservation
    {
        [Key]
        public int IDReservation { get; set; }

        [Required(ErrorMessage = "Campul Titlu este obligatoriu")]
        public string Titlu { get; set; }

        [Required(ErrorMessage = "Campul Descriere este obligatoriu")]
        public string Descriere { get; set; }

        [RegularExpression("1|2|3|4|5|6|7|8|9|10")]
        [Required(ErrorMessage = "Campul Persoane este obligatoriu")]
        public int NrPersoane { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Campul Data este obligatoriu")]
        public DateTime Data { get; set; }


        [ForeignKey("Restaurant")]
        public int IDRestaurant { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        public IEnumerable<SelectListItem> Restaurants { get; set; }

    }
}