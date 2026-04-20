using EncuestasAcme.Filters;
using EncuestasAcme.Services;
using EncuestasAcme.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EncuestasAcme.Controllers
{
    [Authorize]
    [AuthorizeRole("Administrador", "Editor", "Consulta")]
    public class RespuestaController : Controller
    {
        private readonly RespuestaService service;

        public RespuestaController()
        {
            service = new RespuestaService();
        }

        public ActionResult Index()
        {
            var respuestas = service.ObtenerTodas();
            return View(respuestas);
        }

        public ActionResult Detail(int id)
        {
            var respuesta = service.ObtenerDetalle(id);

            if (respuesta == null)
            {
                return HttpNotFound();
            }

            return View(respuesta);
        }
    }
}