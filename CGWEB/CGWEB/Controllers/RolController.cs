using CGWEB.Entities;
using CGWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CGWEB.Controllers
{
    public class RolController : Controller
    {
        RolModel model = new RolModel();

        [HttpGet]
        public ActionResult Rol()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Roles()
        {
            var SESSION_ROL = Session["Rol_id_session"];
            if (SESSION_ROL.ToString() != "1")
            {
                return RedirectToAction("Error404", "Home");
            }
            var roles = model.GetRoles();
            return View(roles) ;
        }

        [HttpPost]
        public ActionResult AddRol(RolEnt entidad)
        {
            var resp = model.AddRol(entidad);

            if (resp > 0)
            {
                ViewBag.MsjPantalla = "Rol agregado corectamente.";
                return RedirectToAction("Roles", "Rol");
            }
            else
            {
                ViewBag.MsjPantalla = "No se ha podido registrar la información";
                return View("Registrarse");
            }
        }

        [HttpGet]
        public ActionResult EditRol(long q)
        {
            var datos = model.GetRol(q);
            var roles = model.GetRoles();

            var listaRoles = new List<SelectListItem>();
            foreach (var item in roles)
            {
                listaRoles.Add(new SelectListItem { Text = item.Rol_name, Value = item.Rol_id.ToString() });
            }

            ViewBag.ListaRoles = listaRoles;
            return View(datos);
        }

        [HttpPost]
        public ActionResult EditRol(RolEnt entidad)
        {
            var resp = model.ValidateRolData(entidad);

            if (resp > 0)
                return RedirectToAction("Roles", "Rol");
            else
            {
                ViewBag.MsjPantalla = "No se ha podido actualizar el rol";
                return View("EditarRol");
            }
        }
    }
}