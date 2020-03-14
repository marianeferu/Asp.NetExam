using Examen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Examen.Controllers
{
    public class ReservationController : Controller
    {

        private RestaurantDBContext db = new RestaurantDBContext();


        // GET: Reservation
        [ActionName("AfisareReservations")]
        public ActionResult Index()
        {
            List<Reservation> reservations = db.Reservations.OrderByDescending(e => e.NrPersoane).ToList();

            if (TempData.ContainsKey("message"))
            {
                ViewBag.message = TempData["message"].ToString();
            }

            ViewBag.Reservations = reservations;
            return View();
        }



        public ActionResult Show(int id)
        {
            Reservation reservation = db.Reservations.Find(id);

            return View(reservation);
        }

        public ActionResult New()
        {
            Reservation reservation = new Reservation();

            reservation.Data = DateTime.Now;
            reservation.Restaurants = GetAllRestaurants();

            return View(reservation);
        }

        [HttpPost]
        public ActionResult New([Bind(Exclude = "Data")] Reservation reservation)
        {
            reservation.Data = DateTime.Now;
            try
            {
                db.Reservations.Add(reservation);
                db.SaveChanges();
                TempData["message"] = "Reservation cu titlul " + reservation.Titlu + " a fost adaugat cu succes";
                return RedirectToAction("AfisareReservations");
            }
            catch (Exception e)
            {
                reservation.Restaurants = GetAllRestaurants();
                return View(reservation);
            }
        }

        [NonAction]
        public IEnumerable<SelectListItem> GetAllRestaurants()
        {
            var selectList = new List<SelectListItem>();

            var restaurants = db.Restaurants.ToList();

            foreach (var restaurant in restaurants)
            {
                selectList.Add(new SelectListItem
                {
                    Value = restaurant.IDRestaurant.ToString(),
                    Text = restaurant.Denumire.ToString()
                });
            }

            return selectList;
        }


        public ActionResult Edit(int id)
        {
            Reservation reservation = db.Reservations.Find(id);

            reservation.Restaurants = GetAllRestaurants();

            return View(reservation);
        }

        [HttpPut]
        public ActionResult Edit(int id, [Bind(Exclude = "Data")] Reservation requestReservation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Reservation Reservation = db.Reservations.Find(id);
                    if (TryUpdateModel(Reservation))
                    {
                        Reservation.Titlu = requestReservation.Titlu;
                        Reservation.Descriere = requestReservation.Descriere;
                        Reservation.NrPersoane = requestReservation.NrPersoane;
                        Reservation.IDRestaurant = requestReservation.IDRestaurant;
                        db.SaveChanges();


                        TempData["message"] = "Rezervarea cu titlul " + Reservation.Titlu + " a fost modificata cu succes";
                    }

                    return RedirectToAction("AfisareReservations");
                }
                else
                {
                    requestReservation.Restaurants = GetAllRestaurants();
                    return View(requestReservation);
                }
            }
            catch (Exception e)
            {
                requestReservation.Restaurants = GetAllRestaurants();
                return View(requestReservation);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            TempData["message"] = "Rezervarea cu titlul " + reservation.Titlu + " a fost stearsa din baza de date";
            db.Reservations.Remove(reservation);
            db.SaveChanges();
            return RedirectToAction("AfisareReservations");
        }
    }



}