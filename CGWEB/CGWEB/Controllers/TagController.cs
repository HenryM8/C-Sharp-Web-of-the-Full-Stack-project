using CGWEB.Entities;
using CGWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CGWEB.Controllers
{
    public class TagController : Controller
    {
        TagModel model = new TagModel();

        [HttpGet]
        public ActionResult Tag()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Tags()
        {
            var SESSION_ROL = Session["Rol_id_session"];
            if (SESSION_ROL.ToString() != "1")
            {
                return RedirectToAction("Error404", "Home");
            }
            var tags = model.GetTags();    
            return View(tags);
        }

        [HttpPost]
        public ActionResult AddTag(TagEnt entidad)
        {
            var resp = model.AddTag(entidad);

            if (resp > 0)
            {
                ViewBag.MsjPantalla = "Etiqueta agregada correctamente.";
                return RedirectToAction("Tags", "Tag");
            }
            else
            {
                ViewBag.MsjPantalla = "No se ha podido registrar la información";
                return View("Registrarse");
            }
        }

        [HttpGet]
        public ActionResult EditTag(long q)
        {
            var datos = model.GetTag(q);
            var roles = model.GetTags();

            var listaTags = new List<SelectListItem>();
            foreach (var item in roles)
            {
                listaTags.Add(new SelectListItem { Text = item.Tag_name, Value = item.Tag_id.ToString() });
            }

            ViewBag.listaTags = listaTags;
            return View(datos);
        }

        [HttpPost]
        public ActionResult EditTag(TagEnt entidad)
        {
            var resp = model.ValidateTagData(entidad);

            if (resp > 0)
                return RedirectToAction("Tags", "Tag");
            else
            {
                ViewBag.MsjPantalla = "No se ha podido actualizar la información.";
                return View("Tags");
            }
        }
    }
}