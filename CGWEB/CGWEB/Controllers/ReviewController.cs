using CGWEB.Entities;
using CGWEB.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CGWEB.Controllers
{
    public class ReviewController : Controller
    {
        ReviewModel model = new ReviewModel();
        RestaurantModel modelRestaurant = new RestaurantModel();

        [HttpGet]
        public ActionResult Review(long q)
        {
            var restaurant = modelRestaurant.GetRestaurant(q);
            var RestaurantList = new List<SelectListItem>();
            RestaurantList.Add(new SelectListItem { Text = restaurant.Restaurant_name, Value = restaurant.Restaurant_id.ToString() });
            ViewBag.RestaurantList = RestaurantList;
            return View();
        }

        [HttpPost]
        public ActionResult AddReview(ReviewEnt reviewEnt)
        {
            reviewEnt.User_id = long.Parse(Session["User_id_session"].ToString());
            reviewEnt.Is_visible = false;
            reviewEnt.Review_date = DateTime.Now;

            var resp = model.AddReview(reviewEnt);

            if (resp > 0)
            {
                ViewBag.MsjPantalla = "Usted ha completado su reseña, pronto será revisada para hacerla visible.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.MsjPantalla = "No se ha podido registrar la información";
                return View("Review");
            }
        }

        [HttpGet]
        public ActionResult Reviews()
        {
            var SESSION_ROL = Session["Rol_id_session"];
            if (SESSION_ROL.ToString() != "1")
            {
                return RedirectToAction("Error404", "Home");
            }
            var datos = model.GetReviews();
            return View(datos);
        }

        [HttpGet]
        public ActionResult ChangeReviewVisibility(long q)
        {
            ReviewEnt entidad = new ReviewEnt();
            entidad.Review_id = q;

            var resp = model.ChangeReviewVisibility(entidad);

            if (resp > 0)
            {
                ViewBag.MsjPantalla = "Se ha actualizado la visualizacion correctamente.";
                return RedirectToAction("Reviews", "Review");
            }
            else
            {
                ViewBag.MsjPantalla = "No se ha podido actualizar la visualizacion";
                return View("Reviews");
            }
        }

        [HttpGet]
        public ActionResult ReviewsByUser(long q)
        {
            var reviews = model.GetReviewsByUser(q);
            return View(reviews);
        }

        [HttpGet]
        public ActionResult EditReview(long q)
        {
            var datos = model.GetReview(q);
            var restaurants = modelRestaurant.GetRestaurants();

            var RestaurantList = new List<SelectListItem>();
            foreach (var item in restaurants)
            {
                RestaurantList.Add(new SelectListItem { Text = item.Restaurant_name, Value = item.Restaurant_id.ToString() });
            }

            ViewBag.RestaurantList = RestaurantList;
            return View(datos);
        }

        [HttpPost]
        public ActionResult EditReview(ReviewEnt entidad)
        {
            entidad.User_id = long.Parse(Session["User_id_session"].ToString());
            entidad.Is_visible = false;
            entidad.Review_date = DateTime.Now;

            var resp = model.ValidateReviewData(entidad);

            if (resp > 0)
                return RedirectToAction("ReviewsByUser", "Review", new { q = long.Parse(Session["User_id_session"].ToString()) });
            else
            {
                ViewBag.MsjPantalla = "No se ha podido actualizar la información del usuario";
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DeleteReview(long q)
        {
            var resp = model.DeleteReviewData(q);

            if (resp > 0)
                return RedirectToAction("ReviewsByUser", "Review", new { q = long.Parse(Session["User_id_session"].ToString()) });
            else
            {
                ViewBag.MsjPantalla = "No se ha podido eliminar.";
                return View("Error");
            }
        }

    }
}