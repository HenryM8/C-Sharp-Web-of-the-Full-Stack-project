using CGWEB.Entities;
using CGWEB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace CGWEB.Controllers
{
    public class UserController : Controller
    {
        UserModel model = new UserModel();
        RolModel rolModel = new RolModel();

        [HttpGet]
        public ActionResult Users()
        {
            var SESSION_ROL = Session["Rol_id_session"];
            if (SESSION_ROL.ToString() != "1")
            {
                return RedirectToAction("Error404", "Home");
            }
            var users = model.GetUsers();
            return View(users);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("InicioSesion", "Home");
        }

        [HttpGet]
        public ActionResult Perfil()
        {
            var idSession = Session["User_id_session"];
            long.TryParse(idSession.ToString(), out long id);
            var user = model.GetUser(id);
            return View(user);
        }

        [HttpGet]
        public ActionResult EditUser(long q)
        {
            var datos = model.GetUser(q);
            return View(datos);
        }

        [HttpPost]
        public ActionResult EditUser(UserEnt entidad, HttpPostedFileBase image)
        {
            entidad.Url_image = string.Empty;
            var resp = model.ValidateUserData(entidad);

            string extensionImagen = Path.GetExtension(Path.GetFileName(image.FileName));
            string root = AppDomain.CurrentDomain.BaseDirectory + "uploads\\users\\" + "user" + entidad.User_id + extensionImagen;
            image.SaveAs(root);

            entidad.Url_image = "\\uploads\\users\\" + "user" + entidad.User_id + extensionImagen;
            model.UpdateUserImage(entidad);

            if (resp > 0)
                return RedirectToAction("Perfil", "User");
            else
            {
                ViewBag.MsjPantalla = "No se ha podido actualizar la información del usuario";
                return View("Perfil");
            }
        }
    }
}