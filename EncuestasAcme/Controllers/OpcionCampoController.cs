using EncuestasAcme.Services;
using EncuestasAcme.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EncuestasAcme.Controllers
{
    public class OpcionCampoController : Controller
    {
        private readonly OpcionCampoService opcionService;
        private readonly CampoEncuestaService campoService;

        public OpcionCampoController()
        {
            opcionService = new OpcionCampoService();
            campoService = new CampoEncuestaService();
        }

        public ActionResult PorCampo(int campoId)
        {
            var campo = campoService.ObtenerPorId(campoId);

            if (campo == null)
            {
                TempData["Error"] = "El campo no existe.";
                return RedirectToAction("Index", "CampoEncuesta");
            }

            var opciones = opcionService.ObtenerPorCampo(campoId);
            ViewBag.CampoId = campoId;
            ViewBag.CampoTitulo = campo.CAM_Titulo_Visible;
            ViewBag.EncuestaId = campo.ENC_Encuesta;

            return View(opciones);
        }

        public ActionResult Create(int campoId)
        {
            var vm = opcionService.ConstruirFormularioCrear(campoId);

            if (vm == null)
            {
                TempData["Error"] = "El campo no existe o no permite opciones.";
                return RedirectToAction("Index", "CampoEncuesta");
            }

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OpcionCampoFormViewModel vm)
        {
            try
            {
                if (vm == null || vm.CreateDTO == null)
                {
                    TempData["Error"] = "Los datos de la opción son obligatorios.";
                    return RedirectToAction("Index", "CampoEncuesta");
                }

                if (!ModelState.IsValid)
                {
                    var formVm = opcionService.ConstruirFormularioCrear(vm.CreateDTO.CAM_Campo);

                    if (formVm == null)
                    {
                        TempData["Error"] = "El campo no existe o no permite opciones.";
                        return RedirectToAction("Index", "CampoEncuesta");
                    }

                    formVm.CreateDTO = vm.CreateDTO;
                    return View(formVm);
                }

                opcionService.Crear(vm.CreateDTO);
                TempData["Success"] = "Opción creada correctamente.";

                return RedirectToAction("PorCampo", new { campoId = vm.CreateDTO.CAM_Campo });
            }
            catch (Exception ex)
            {
                var formVm = opcionService.ConstruirFormularioCrear(vm.CreateDTO.CAM_Campo);

                if (formVm == null)
                {
                    TempData["Error"] = ex.Message;
                    return RedirectToAction("Index", "CampoEncuesta");
                }

                formVm.CreateDTO = vm.CreateDTO;
                ModelState.AddModelError("", ex.Message);
                return View(formVm);
            }
        }

        public ActionResult Edit(int id)
        {
            var vm = opcionService.ConstruirFormularioEditar(id);

            if (vm == null)
            {
                TempData["Error"] = "La opción no existe.";
                return RedirectToAction("Index", "CampoEncuesta");
            }

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OpcionCampoFormViewModel vm)
        {
            try
            {
                if (vm == null || vm.UpdateDTO == null)
                {
                    TempData["Error"] = "Los datos de la opción son obligatorios.";
                    return RedirectToAction("Index", "CampoEncuesta");
                }

                if (!ModelState.IsValid)
                {
                    var formVm = opcionService.ConstruirFormularioEditar(vm.UpdateDTO.OPC_Opcion);

                    if (formVm == null)
                    {
                        TempData["Error"] = "La opción no existe.";
                        return RedirectToAction("Index", "CampoEncuesta");
                    }

                    formVm.UpdateDTO = vm.UpdateDTO;
                    return View(formVm);
                }

                var campoId = opcionService.Actualizar(vm.UpdateDTO);
                TempData["Success"] = "Opción actualizada correctamente.";

                return RedirectToAction("PorCampo", new { campoId });
            }
            catch (Exception ex)
            {
                var formVm = opcionService.ConstruirFormularioEditar(vm.UpdateDTO.OPC_Opcion);

                if (formVm == null)
                {
                    TempData["Error"] = ex.Message;
                    return RedirectToAction("Index", "CampoEncuesta");
                }

                formVm.UpdateDTO = vm.UpdateDTO;
                ModelState.AddModelError("", ex.Message);
                return View(formVm);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var campoId = opcionService.EliminarLogico(id);
                TempData["Success"] = "Opción eliminada correctamente.";

                return RedirectToAction("PorCampo", new { campoId });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "CampoEncuesta");
            }
        }
    }
}