using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EncuestasAcme.Services;

namespace EncuestasAcme.Controllers
{
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