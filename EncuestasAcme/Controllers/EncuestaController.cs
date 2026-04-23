using EncuestasAcme.DTOs.Encuesta;
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

        public ActionResult ResponderInterno(int id)
        {
            var encuesta = service.ObtenerDetalle(id);

            if (encuesta == null)
            {
                return HttpNotFound();
            }

            var vm = new ResponderEncuestaFormViewModel
            {
                Encuesta = encuesta,
                Respuesta = new ResponderEncuestaDTO
                {
                    ENC_Encuesta = encuesta.ENC_Encuesta,
                    Campos = encuesta.Campos.Select(c => new ResponderEncuestaCampoDTO
                    {
                        CAM_Campo = c.CAM_Campo,
                        TCA_Clave = c.TCA_Clave
                    }).ToList()
                }
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResponderInterno(ResponderEncuestaFormViewModel vm)
        {
            try
            {
                if (vm == null || vm.Respuesta == null)
                {
                    TempData["Error"] = "La respuesta es obligatoria.";
                    return RedirectToAction("Index");
                }

                var encuesta = service.ObtenerDetalle(vm.Respuesta.ENC_Encuesta);

                if (encuesta == null)
                {
                    TempData["Error"] = "La encuesta no existe.";
                    return RedirectToAction("Index");
                }

                if (!ModelState.IsValid)
                {
                    vm.Encuesta = encuesta;
                    return View(vm);
                }

                var respuestaInternaService = new EncuestaRespuestaInternaService();

                respuestaInternaService.GuardarRespuesta(
                    vm.Respuesta,
                    Request.UserHostAddress,
                    Request.UserAgent
                );

                TempData["Success"] = "Encuesta respondida correctamente.";
                return RedirectToAction("Index", "Respuesta");
            }
            catch (Exception ex)
            {
                var encuesta = service.ObtenerDetalle(vm.Respuesta.ENC_Encuesta);
                vm.Encuesta = encuesta;
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }
    }
}