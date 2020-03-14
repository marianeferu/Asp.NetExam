using Examen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Examen.Controllers
{
    public class RestaurantController : Controller
    {
        // GET: Restaurant
        private RestaurantDBContext db = new RestaurantDBContext();
        public ActionResult Index()
        {
            List<Restaurant> restaurants = db.Restaurants.ToList();

            ViewBag.Restaurants = restaurants;
            return View();
        }

        public ActionResult Show(int id)
        {
            Restaurant restaurant = db.Restaurants.Find(id);

            return View(restaurant);
        }
    }
}