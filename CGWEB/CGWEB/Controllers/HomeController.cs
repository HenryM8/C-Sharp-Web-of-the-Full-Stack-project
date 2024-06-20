using CGWEB.Entities;
using CGWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CGWEB.Controllers
{
    public class HomeController : Controller
    {
        RestaurantModel model = new RestaurantModel();
        UserModel user = new UserModel();
        ReviewModel reviewModel = new ReviewModel();
        RestaurantTagModel restaurantTagModel = new RestaurantTagModel();
        TagModel tagModel = new TagModel();

        [HttpGet]
        public ActionResult InicioSesion()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserEnt entidad)
        {
            try
            {
                var resp = user.Login(entidad);

                if (resp != null)
                {
                    Session["User_id_session"] = resp.User_id.ToString();
                    Session["Email_session"] = resp.Mail;
                    Session["Username_session"] = resp.Username;
                    Session["Rol_name_session"] = resp.Rol_name;
                    Session["User_Img_session"] = resp.Url_image;
                    Session["Rol_id_session"] = resp.Rol_id.ToString();
                    Session["User_register_date_session"] = resp.Register_date.ToString();
                    Session["User_date_recovery"] = resp.Date_recovery.ToString();
                    Session["Token_session"] = resp.Token;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.MsjPantalla = "No se ha podido validar su información";
                    return View("InicioSesion");
                }
            }
            catch (Exception ex)
            {
                ViewBag.MsjPantalla = "Consulta con el administrador del sistema";
                return View("InicioSesion");
            }
        }

        [HttpGet]
        public ActionResult Registrarse()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(UserEnt entidad)
        {
            entidad.Rol_id = 2;
            entidad.Url_image = "\\uploads\\users\\default.jpg";
            entidad.Register_date = DateTime.Now;
            entidad.Date_recovery = DateTime.Now;
            entidad.Use_recovery_password = false;

            var resp = user.AddUser(entidad);

            if (resp > 0)
            {
                ViewBag.MsjPantalla = "Usted ha creado exitosamente su usuario.";
                return RedirectToAction("InicioSesion", "Home");
            }
            else
            {
                ViewBag.MsjPantalla = "No se ha podido registrar la información";
                return View("Registrarse");
            }
        }

        [HttpGet]
        public ActionResult RecuperarContrasenna()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecoveryPassword(UserEnt entidad)
        {
            var resp = user.RecoveryPassword(entidad);

            if (resp)
                return RedirectToAction("InicioSesion", "Home");
            else
            {
                ViewBag.MsjPantalla = "No se ha podido recuperar su información";
                return View("RecuperarContrasenna");
            }
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(UserEnt entidad)
        {
            entidad.User_id = long.Parse(Session["User_id_session"].ToString());
            entidad.Username = Session["Username_session"].ToString();
            var isUser = user.Login(entidad);

            if (isUser == null)
            {
                ViewBag.MsjPantalla = "La contraseña actual es incorrecta";
                return View("ChangePassword");
            }

            if (entidad.New_password != entidad.Confirm_new_password)
            {
                ViewBag.MsjPantalla = "Las contraseñas no coinciden";
                return View("ChangePassword");
            }

            if (entidad.Password == entidad.New_password)
            {
                ViewBag.MsjPantalla = "Debe ingresar una contraseña diferente a la actual";
                return View("ChangePassword");
            }

            var changePassword = user.ChangePassword(entidad);

            if (changePassword > 0)
                return RedirectToAction("Index", "Home");
            else
            {
                ViewBag.MsjPantalla = "No se ha podido actualizar su contraseña";
                return View("ChangePassword");
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            var datos = model.GetRestaurants();
            var userReviews = reviewModel.GetReviewsByUser(long.Parse(Session["User_id_session"].ToString()));
            ViewBag.ReviewList = userReviews;
            return View(datos);
        }

        [HttpGet]
        public ActionResult Error404()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Graficos()
        {
            var SESSION_ROL = Session["Rol_id_session"];
            if (SESSION_ROL.ToString() != "1")
            {
                return RedirectToAction("Error404", "Home");
            }
            ViewBag.Message = "Your contact page.";

            var restaurants = model.GetRestaurants();
            var users = user.GetUsers();
            var restaurantTags = restaurantTagModel.GetRestaurantTags();
            var tags = tagModel.GetTags();
            var reviews = reviewModel.GetReviews();

            ViewBag.RestaurantTagsList = restaurantTags;
            ViewBag.TagsList = tags;
            ViewBag.UserList = users;
            ViewBag.UserCount = users.Count;
            ViewBag.RestaurantCount = restaurants.Count;
            ViewBag.ReviewCount = reviews.Count;

            return View(restaurants);
        }
    }
}