using EncuestasAcme.Filters;
using EncuestasAcme.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EncuestasAcme.Controllers
{
    [Authorize]
    [AuthorizeRole("Administrador", "Editor", "Consulta")]
    public class DashboardController : Controller
    {
        private readonly DashboardService service;

        public DashboardController()
        {
            service = new DashboardService();
        }

        public ActionResult Index()
        {
            var vm = service.ObtenerDashboard();
            return View(vm);
        }
    }
}