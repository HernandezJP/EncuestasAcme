using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EncuestasAcme.Data;

namespace EncuestasAcme.Controllers
{
    public class TestController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new ApplicationDbContext())
            {
                var totalUsuarios = db.Usuarios.Count();
                return Content("Conexión OK. Usuarios registrados: " + totalUsuarios);
            }
        }
    }
}