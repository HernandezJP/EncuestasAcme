using EncuestasAcme.DTOs.TipoCampo;
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
    [AuthorizeRole("Administrador", "Editor")]
    public class TipoCampoController : Controller
    {
        private readonly TipoCampoService service;

        public TipoCampoController()
        {
            service = new TipoCampoService();
        }

        public ActionResult Index()
        {
            var tipos = service.ObtenerTodos();
            return View(tipos);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateTipoCampoDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(dto);
                }

                service.Crear(dto);
                TempData["Success"] = "Tipo de campo creado correctamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(dto);
            }
        }

        public ActionResult Detail(int id)
        {
            var tipoCampo = service.ObtenerPorId(id);

            if (tipoCampo == null)
            {
                TempData["Error"] = "El tipo de campo no fue encontrado.";
                return RedirectToAction("Index");
            }

            var dto = new ResponseTipoCampoDTO
            {
                TCA_Tipo_Campo = tipoCampo.TCA_Tipo_Campo,
                TCA_Codigo = tipoCampo.TCA_Codigo,
                TCA_Clave = tipoCampo.TCA_Clave,
                TCA_Descripcion = tipoCampo.TCA_Descripcion,
                TCA_Permite_Opciones = tipoCampo.TCA_Permite_Opciones,
                TCA_Permite_Multiples = tipoCampo.TCA_Permite_Multiples,
                TCA_Estado = tipoCampo.TCA_Estado,
                TCA_Fecha_Creacion = tipoCampo.TCA_Fecha_Creacion
            };

            return View(dto);
        }
        public ActionResult Edit(int id)
        {
            var tipoCampo = service.ObtenerPorId(id);

            if (tipoCampo == null)
            {
                return HttpNotFound();
            }

            var dto = new UpdateTipoCampoDTO
            {
                TCA_Tipo_Campo = tipoCampo.TCA_Tipo_Campo,
                TCA_Clave = tipoCampo.TCA_Clave,
                TCA_Descripcion = tipoCampo.TCA_Descripcion,
                TCA_Permite_Opciones = tipoCampo.TCA_Permite_Opciones,
                TCA_Permite_Multiples = tipoCampo.TCA_Permite_Multiples
            };

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UpdateTipoCampoDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(dto);
                }

                service.Actualizar(dto);
                TempData["Success"] = "Tipo de campo actualizado correctamente.";
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
                TempData["Success"] = "Tipo de campo inactivado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        public ActionResult Reactivar(int id)
        {
            try
            {
                service.Reactivar(id);
                TempData["Success"] = "Tipo de campo reactivado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}