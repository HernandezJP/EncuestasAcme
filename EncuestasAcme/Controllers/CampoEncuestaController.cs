using EncuestasAcme.DTOs.CampoEncuesta;
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
    [AuthorizeRole("Administrador", "Editor")]
    public class CampoEncuestaController : Controller
    {
        private readonly CampoEncuestaService campoService;
        private readonly TipoCampoService tipoCampoService;
        private readonly EncuestaService encuestaService;

        public CampoEncuestaController()
        {
            campoService = new CampoEncuestaService();
            tipoCampoService = new TipoCampoService();
            encuestaService = new EncuestaService();
        }

        public ActionResult Index()
        {
            var campos = campoService.ObtenerTodos();
            return View(campos);
        }

        public ActionResult Create(int encuestaId)
        {
            var encuesta = encuestaService.ObtenerPorId(encuestaId);

            if (encuesta == null)
            {
                TempData["Error"] = "La encuesta no existe.";
                return RedirectToAction("Index", "Encuesta");
            }

            var vm = new CampoEncuestaFormViewModel
            {
                ENC_Encuesta = encuesta.ENC_Encuesta,
                ENC_Nombre = encuesta.ENC_Nombre,
                CrearDTO = new CreateCampoEncuestaDTO
                {
                    ENC_Encuesta = encuesta.ENC_Encuesta,
                    CAM_Es_Requerido = false,
                    CAM_Orden = 1
                },
                TiposCampo = tipoCampoService.ObtenerActivos()
                    .Select(x => new SelectListItem
                    {
                        Value = x.TCA_Tipo_Campo.ToString(),
                        Text = x.TCA_Descripcion
                    })
                    .ToList()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CampoEncuestaFormViewModel vm)
        {
            try
            {
                if (vm == null || vm.CrearDTO == null)
                {
                    TempData["Error"] = "Los datos del campo son obligatorios.";
                    return RedirectToAction("Index", "Encuesta");
                }

                if (!ModelState.IsValid)
                {
                    var encuesta = encuestaService.ObtenerPorId(vm.CrearDTO.ENC_Encuesta);

                    vm.ENC_Encuesta = vm.CrearDTO.ENC_Encuesta;
                    vm.ENC_Nombre = encuesta != null ? encuesta.ENC_Nombre : string.Empty;
                    vm.TiposCampo = tipoCampoService.ObtenerActivos()
                        .Select(x => new SelectListItem
                        {
                            Value = x.TCA_Tipo_Campo.ToString(),
                            Text = x.TCA_Descripcion
                        })
                        .ToList();

                    return View(vm);
                }

                campoService.Crear(vm.CrearDTO);
                TempData["Success"] = "Campo creado correctamente.";

                return RedirectToAction("Detail", "Encuesta", new { id = vm.CrearDTO.ENC_Encuesta });
            }
            catch (Exception ex)
            {
                var encuesta = encuestaService.ObtenerPorId(vm.CrearDTO.ENC_Encuesta);

                vm.ENC_Encuesta = vm.CrearDTO.ENC_Encuesta;
                vm.ENC_Nombre = encuesta != null ? encuesta.ENC_Nombre : string.Empty;
                vm.TiposCampo = tipoCampoService.ObtenerActivos()
                    .Select(x => new SelectListItem
                    {
                        Value = x.TCA_Tipo_Campo.ToString(),
                        Text = x.TCA_Descripcion
                    })
                    .ToList();

                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }

        public ActionResult Edit(int id)
        {
            var campo = campoService.ObtenerPorId(id);

            if (campo == null)
            {
                TempData["Error"] = "El campo no existe.";
                return RedirectToAction("Index", "Encuesta");
            }

            var encuesta = encuestaService.ObtenerPorId(campo.ENC_Encuesta);

            var vm = new CampoEncuestaFormViewModel
            {
                ENC_Encuesta = campo.ENC_Encuesta,
                ENC_Nombre = encuesta != null ? encuesta.ENC_Nombre : string.Empty,
                ActualizarDTO = new UpdateCampoEncuestaDTO
                {
                    CAM_Campo = campo.CAM_Campo,
                    TCA_Tipo_Campo = campo.TCA_Tipo_Campo,
                    CAM_Nombre_Interno = campo.CAM_Nombre_Interno,
                    CAM_Titulo_Visible = campo.CAM_Titulo_Visible,
                    CAM_Es_Requerido = campo.CAM_Es_Requerido,
                    CAM_Orden = campo.CAM_Orden
                },
                TiposCampo = tipoCampoService.ObtenerActivos()
                    .Select(x => new SelectListItem
                    {
                        Value = x.TCA_Tipo_Campo.ToString(),
                        Text = x.TCA_Descripcion
                    })
                    .ToList()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CampoEncuestaFormViewModel vm)
        {
            try
            {
                if (vm == null || vm.ActualizarDTO == null)
                {
                    TempData["Error"] = "Los datos del campo son obligatorios.";
                    return RedirectToAction("Index", "Encuesta");
                }

                if (!ModelState.IsValid)
                {
                    var campo = campoService.ObtenerPorId(vm.ActualizarDTO.CAM_Campo);
                    var encuesta = campo != null ? encuestaService.ObtenerPorId(campo.ENC_Encuesta) : null;

                    vm.ENC_Encuesta = campo != null ? campo.ENC_Encuesta : 0;
                    vm.ENC_Nombre = encuesta != null ? encuesta.ENC_Nombre : string.Empty;
                    vm.TiposCampo = tipoCampoService.ObtenerActivos()
                        .Select(x => new SelectListItem
                        {
                            Value = x.TCA_Tipo_Campo.ToString(),
                            Text = x.TCA_Descripcion
                        })
                        .ToList();

                    return View(vm);
                }

                var encuestaId = campoService.Actualizar(vm.ActualizarDTO);
                TempData["Success"] = "Campo actualizado correctamente.";

                return RedirectToAction("Detail", "Encuesta", new { id = encuestaId });
            }
            catch (Exception ex)
            {
                var campo = campoService.ObtenerPorId(vm.ActualizarDTO.CAM_Campo);
                var encuesta = campo != null ? encuestaService.ObtenerPorId(campo.ENC_Encuesta) : null;

                vm.ENC_Encuesta = campo != null ? campo.ENC_Encuesta : 0;
                vm.ENC_Nombre = encuesta != null ? encuesta.ENC_Nombre : string.Empty;
                vm.TiposCampo = tipoCampoService.ObtenerActivos()
                    .Select(x => new SelectListItem
                    {
                        Value = x.TCA_Tipo_Campo.ToString(),
                        Text = x.TCA_Descripcion
                    })
                    .ToList();

                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var encuestaId = campoService.EliminarLogico(id);
                TempData["Success"] = "Campo eliminado correctamente.";

                return RedirectToAction("Detalle", "Encuesta", new { id = encuestaId });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Encuesta");
            }
        }
    }
}