using CGWEB.Entities;
using CGWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace CGWEB.Controllers
{
    public class RestaurantTagController : Controller
    {
        RestaurantTagModel model = new RestaurantTagModel();
        RestaurantModel modelRestaurant = new RestaurantModel();
        TagModel tagModel = new TagModel();

        [HttpGet]
        public ActionResult RestaurantTag()
        {
            var restaurants = modelRestaurant.GetRestaurants();
            var tags = tagModel.GetTags();

            var RestaurantList = new List<SelectListItem>();
            foreach (var item in restaurants)
            {
                RestaurantList.Add(new SelectListItem { Text = item.Restaurant_name, Value = item.Restaurant_id.ToString() });
            }

            var TagList = new List<SelectListItem>();
            foreach (var item in tags)
            {
                TagList.Add(new SelectListItem { Text = item.Tag_name, Value = item.Tag_id.ToString() });
            }

            ViewBag.RestaurantList = RestaurantList;
            ViewBag.TagList = TagList;
            return View();
        }

        [HttpGet]
        public ActionResult RestaurantTags()
        {
            var SESSION_ROL = Session["Rol_id_session"];
            if (SESSION_ROL.ToString() != "1")
            {
                return RedirectToAction("Error404", "Home");
            }
            var datos = model.GetRestaurantTags();
            return View(datos);
        }

        [HttpPost]
        public ActionResult AddRestaurantTag(RestaurantTagEnt entidad)
        {
            var resp = model.AddRestaurantTag(entidad);

            if (resp > 0)
            {
                ViewBag.MsjPantalla = "Se ha etiquetado al restaurante correctamente."; 
                return RedirectToAction("RestaurantTags", "RestaurantTag");
            }
            else
            {
                ViewBag.MsjPantalla = "No se ha podido registrar la información";
                return View("RestaurantTag");
            }
        }

        [HttpGet]
        public ActionResult EditRestaurantTag(long q)
        {
            var datos = model.GetRestaurantTag(q);
            var restaurants = modelRestaurant.GetRestaurants();
            var tags = tagModel.GetTags();

            var RestaurantList = new List<SelectListItem>();
            foreach (var item in restaurants)
            {
                RestaurantList.Add(new SelectListItem { Text = item.Restaurant_name, Value = item.Restaurant_id.ToString() });
            }

            var TagList = new List<SelectListItem>();
            foreach (var item in tags)
            {
                TagList.Add(new SelectListItem { Text = item.Tag_name, Value = item.Tag_id.ToString() });
            }

            ViewBag.RestaurantList = RestaurantList;
            ViewBag.TagList = TagList;
            return View(datos);
        }

        [HttpPost]
        public ActionResult EditRestaurantTag(RestaurantTagEnt entidad)
        {
            var resp = model.ValidateRestaurantTagData(entidad);

            if (resp > 0)
                return RedirectToAction("RestaurantTags", "RestaurantTag");
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
    }
}