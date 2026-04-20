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
    [AuthorizeRole("Administrador")]
    public class UsuarioController : Controller
    {
        private readonly UsuarioService service;

        public UsuarioController()
        {
            service = new UsuarioService();
        }

        public ActionResult Index()
        {
            var usuarios = service.ObtenerTodos();
            return View(usuarios);
        }

        public ActionResult Create()
        {
            var vm = service.ConstruirFormularioCrear();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuarioFormViewModel vm)
        {
            try
            {
                if (vm == null || vm.CreateDTO == null)
                {
                    TempData["Error"] = "Los datos del usuario son obligatorios.";
                    return RedirectToAction("Index");
                }

                if (!ModelState.IsValid)
                {
                    var formVm = service.ConstruirFormularioCrear();
                    formVm.CreateDTO = vm.CreateDTO;
                    return View(formVm);
                }

                service.Crear(vm.CreateDTO);
                TempData["Success"] = "Usuario creado correctamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var formVm = service.ConstruirFormularioCrear();
                formVm.CreateDTO = vm.CreateDTO;
                ModelState.AddModelError("", ex.Message);
                return View(formVm);
            }
        }

        public ActionResult Edit(int id)
        {
            var vm = service.ConstruirFormularioEditar(id);

            if (vm == null)
            {
                TempData["Error"] = "El usuario no existe.";
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UsuarioFormViewModel vm)
        {
            try
            {
                if (vm == null || vm.UpdateDTO == null)
                {
                    TempData["Error"] = "Los datos del usuario son obligatorios.";
                    return RedirectToAction("Index");
                }

                if (!ModelState.IsValid)
                {
                    var formVm = service.ConstruirFormularioEditar(vm.UpdateDTO.USU_Usuario);
                    if (formVm == null)
                    {
                        TempData["Error"] = "El usuario no existe.";
                        return RedirectToAction("Index");
                    }

                    formVm.UpdateDTO = vm.UpdateDTO;
                    return View(formVm);
                }

                service.Actualizar(vm.UpdateDTO);
                TempData["Success"] = "Usuario actualizado correctamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var formVm = service.ConstruirFormularioEditar(vm.UpdateDTO.USU_Usuario);
                if (formVm == null)
                {
                    TempData["Error"] = ex.Message;
                    return RedirectToAction("Index");
                }

                formVm.UpdateDTO = vm.UpdateDTO;
                ModelState.AddModelError("", ex.Message);
                return View(formVm);
            }
        }

        public ActionResult Detail(int id)
        {
            var usuario = service.ObtenerDetalle(id);

            if (usuario == null)
            {
                TempData["Error"] = "El usuario no existe.";
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                service.EliminarLogico(id);
                TempData["Success"] = "Usuario inactivado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        public ActionResult Activar(int id)
        {
            try
            {
                service.ActivarLogico(id);
                TempData["Success"] = "Usuario activado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}