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
    public class RolController : Controller
    {
        private readonly RolService service;

        public RolController()
        {
            service = new RolService();
        }

        public ActionResult Index()
        {
            var roles = service.ObtenerTodos();
            return View(roles);
        }

        public ActionResult Create()
        {
            var vm = service.ConstruirFormularioCrear();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RolFormViewModel vm)
        {
            try
            {
                if (vm == null || vm.CreateDTO == null)
                {
                    TempData["Error"] = "Los datos del rol son obligatorios.";
                    return RedirectToAction("Index");
                }

                if (!ModelState.IsValid)
                {
                    return View(vm);
                }

                service.Crear(vm.CreateDTO);
                TempData["Success"] = "Rol creado correctamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }

        public ActionResult Edit(int id)
        {
            var vm = service.ConstruirFormularioEditar(id);

            if (vm == null)
            {
                TempData["Error"] = "El rol no existe.";
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RolFormViewModel vm)
        {
            try
            {
                if (vm == null || vm.UpdateDTO == null)
                {
                    TempData["Error"] = "Los datos del rol son obligatorios.";
                    return RedirectToAction("Index");
                }

                if (!ModelState.IsValid)
                {
                    return View(vm);
                }

                service.Actualizar(vm.UpdateDTO);
                TempData["Success"] = "Rol actualizado correctamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }

        public ActionResult Detail(int id)
        {
            var rol = service.ObtenerDetalle(id);

            if (rol == null)
            {
                TempData["Error"] = "El rol no existe.";
                return RedirectToAction("Index");
            }

            return View(rol);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                service.EliminarLogico(id);
                TempData["Success"] = "Rol inactivado correctamente.";
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
                service.Activar(id);
                TempData["Success"] = "Rol activado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}