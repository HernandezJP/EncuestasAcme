using EncuestasAcme.DTOs.Encuesta;
using EncuestasAcme.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EncuestasAcme.Controllers
{
    public class EncuestaController : Controller
    {
        private readonly EncuestaService service;

        public EncuestaController()
        {
            service = new EncuestaService();
        }

        public ActionResult Index()
        {
            var encuestas = service.ObtenerTodas();
            return View(encuestas);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Detail(int id)
        {
            var encuesta = service.ObtenerDetalle(id);

            if (encuesta == null)
            {
                return HttpNotFound();
            }

            return View(encuesta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateEncuestaDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(dto);
                }

                service.Crear(dto);
                TempData["Success"] = "Encuesta creada correctamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(dto);
            }
        }

        public ActionResult Edit(int id)
        {
            var encuesta = service.ObtenerPorId(id);

            if (encuesta == null)
            {
                return HttpNotFound();
            }

            var dto = new UpdateEncuestaDTO
            {
                ENC_Encuesta = encuesta.ENC_Encuesta,
                ENC_Nombre = encuesta.ENC_Nombre,
                ENC_Descripcion = encuesta.ENC_Descripcion
            };

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UpdateEncuestaDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(dto);
                }

                service.Actualizar(dto);
                TempData["Success"] = "Encuesta actualizada correctamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(dto);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                service.EliminarLogico(id);
                TempData["Success"] = "Encuesta eliminada correctamente.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}