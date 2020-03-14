using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Examen.Models
{
    public class Restaurant
    {
        [Key]
        public int IDRestaurant { get; set; }

        [Required(ErrorMessage = "Restaurantul trebuie sa aiba o denumire")]
        public string Denumire { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }

    }

    public class RestaurantDBContext : DbContext
    {

        static RestaurantDBContext()
        { Database.SetInitializer<RestaurantDBContext>(new DropCreateDatabaseIfModelChanges<RestaurantDBContext>()); }
        public RestaurantDBContext() : base("DBConnectionString") { }

        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        
    }
}
