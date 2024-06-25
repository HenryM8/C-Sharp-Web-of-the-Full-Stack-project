using CGWEB.Entities;
using CGWEB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace CGWEB.Controllers
{
    public class RestaurantController : Controller
    {
        RestaurantModel model = new RestaurantModel();
        RestaurantTagModel restaurantTagModel = new RestaurantTagModel();
        ReviewModel reviewModel = new ReviewModel();

        [HttpGet]
        public ActionResult Restaurant()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddRestaurant(RestaurantEnt entidad, HttpPostedFileBase image)
        {
            entidad.Is_visible = false;
            entidad.Url_image = string.Empty;
            var IdRestaurant = model.AddRestaurant(entidad);

            string extensionImagen = Path.GetExtension(Path.GetFileName(image.FileName));
            string root = AppDomain.CurrentDomain.BaseDirectory + "uploads\\restaurants\\" + "restaurant" + IdRestaurant + extensionImagen;
            image.SaveAs(root);

            entidad.Restaurant_id = IdRestaurant;
            entidad.Url_image = "\\uploads\\restaurants\\" + "restaurant" + IdRestaurant + extensionImagen;
            model.UpdateRestaurantImage(entidad);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Restaurants()
        {
            var SESSION_ROL = Session["Rol_id_session"];
            if (SESSION_ROL.ToString() != "1")
            {
                return RedirectToAction("Error404", "Home");
            }
            var restaurants = model.GetRestaurants();
            return View(restaurants);
        }

        [HttpGet]
        public ActionResult ViewRestaurant(long q)
        {
            RestaurantEnt resp = model.GetRestaurant(q);

            var tags = restaurantTagModel.GetRestaurantTagsByRestaurantId(q);
            ViewBag.TagList = tags;

            var reviews = reviewModel.GetReviewsByRestaurantId(q);
            ViewBag.ReviewList = reviews;

            return View(resp);
        }

        [HttpGet]
        public ActionResult ChangeRestaurantVisibility(long q)
        {
            RestaurantEnt entidad = new RestaurantEnt();
            entidad.Restaurant_id = q;

            var resp = model.ChangeRestaurantVisibility(entidad);

            if (resp > 0)
            {
                ViewBag.MsjPantalla = "Se ha actualizado la visualizacion correctamente.";
                return RedirectToAction("Restaurants", "Restaurant");
            }
            else
            {
                ViewBag.MsjPantalla = "No se ha podido actualizar la visualizacion";
                return View("Restaurants");
            }
        }

        [HttpGet]
        public ActionResult EditRestaurant(long q)
        {
            var datos = model.GetRestaurant(q);
            var roles = model.GetRestaurants();

            var listaRoles = new List<SelectListItem>();
            foreach (var item in roles)
            {
                listaRoles.Add(new SelectListItem { Text = item.Restaurant_name, Value = item.Restaurant_id.ToString() });
            }

            ViewBag.ListaRoles = listaRoles;
            return View(datos);
        }

        [HttpPost]
        public ActionResult EditRestaurant(RestaurantEnt entidad, HttpPostedFileBase image)
        {
            entidad.Url_image = string.Empty;
            model.ValidateRestaurantData(entidad);
            if (image != null)
            {
                string extensionImagen = Path.GetExtension(Path.GetFileName(image.FileName));
                string root = AppDomain.CurrentDomain.BaseDirectory + "uploads\\restaurants\\" + "restaurant" + entidad.Restaurant_id + extensionImagen;
                image.SaveAs(root);
                entidad.Url_image = "\\uploads\\restaurants\\" + "restaurant" + entidad.Restaurant_id + extensionImagen;
                model.UpdateRestaurantImage(entidad);
            }
            return RedirectToAction("Restaurants", "Restaurant");
        }

        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }
    }
}