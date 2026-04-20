using EncuestasAcme.Services;
using EncuestasAcme.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;

namespace EncuestasAcme.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly AuthService service;

        public AuthController()
        {
            service = new AuthService();
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View(new LoginFormViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginFormViewModel vm)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Dashboard");
                }

                if (vm == null || vm.LoginDTO == null)
                {
                    ModelState.AddModelError("", "Debe ingresar sus credenciales.");
                    return View(new LoginFormViewModel());
                }

                if (!ModelState.IsValid)
                {
                    return View(vm);
                }

                var usuario = service.Login(vm.LoginDTO);

                FormsAuthentication.SignOut();
                Session.Clear();

                FormsAuthentication.SetAuthCookie(usuario.USU_User_Name, false);

                Session["USU_Usuario"] = usuario.USU_Usuario;
                Session["USU_Codigo"] = usuario.USU_Codigo;
                Session["USU_User_Name"] = usuario.USU_User_Name;
                Session["USU_NombreCompleto"] = usuario.NombreCompleto;
                Session["USU_Correo"] = usuario.USU_Correo_Electronico;
                Session["ROL_Rol"] = usuario.ROL_Rol;
                Session["ROL_Nombre"] = usuario.ROL_Nombre;

                return RedirectToAction("Index", "Dashboard");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm ?? new LoginFormViewModel());
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login", "Auth");
        }

        [Authorize]
        public ActionResult AccesoDenegado()
        {
            return View();
        }
    }
}