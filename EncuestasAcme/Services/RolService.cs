using EncuestasAcme.DTOs.Rol;
using EncuestasAcme.Interfaces;
using EncuestasAcme.Models;
using EncuestasAcme.Repositories;
using EncuestasAcme.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncuestasAcme.Services
{
    public class RolService
    {
        private readonly IRolRepository repository;

        public RolService()
        {
            repository = new RolRepository();
        }

        public List<ResponseRolDTO> ObtenerTodos()
        {
            return repository.ObtenerTodos()
                .Select(x => new ResponseRolDTO
                {
                    ROL_Rol = x.ROL_Rol,
                    ROL_Codigo = x.ROL_Codigo,
                    ROL_Nombre = x.ROL_Nombre,
                    ROL_Descripcion = x.ROL_Descripcion,
                    ROL_Estado = x.ROL_Estado,
                    ROL_Fecha_Creacion = x.ROL_Fecha_Creacion
                })
                .ToList();
        }

        public ResponseRolDTO ObtenerDetalle(int id)
        {
            var rol = repository.ObtenerPorId(id);

            if (rol == null)
            {
                return null;
            }

            return new ResponseRolDTO
            {
                ROL_Rol = rol.ROL_Rol,
                ROL_Codigo = rol.ROL_Codigo,
                ROL_Nombre = rol.ROL_Nombre,
                ROL_Descripcion = rol.ROL_Descripcion,
                ROL_Estado = rol.ROL_Estado,
                ROL_Fecha_Creacion = rol.ROL_Fecha_Creacion
            };
        }

        public List<ResponseRolDTO> ObtenerActivos()
        {
            return repository.ObtenerActivos()
                .Select(x => new ResponseRolDTO
                {
                    ROL_Rol = x.ROL_Rol,
                    ROL_Codigo = x.ROL_Codigo,
                    ROL_Nombre = x.ROL_Nombre,
                    ROL_Descripcion = x.ROL_Descripcion,
                    ROL_Estado = x.ROL_Estado,
                    ROL_Fecha_Creacion = x.ROL_Fecha_Creacion
                })
                .ToList();
        }

        public ACE_ROL ObtenerPorId(int id)
        {
            return repository.ObtenerPorId(id);
        }

        public RolFormViewModel ConstruirFormularioCrear()
        {
            return new RolFormViewModel();
        }

        public RolFormViewModel ConstruirFormularioEditar(int rolId)
        {
            var rol = repository.ObtenerPorId(rolId);

            if (rol == null)
            {
                return null;
            }

            return new RolFormViewModel
            {
                UpdateDTO = new UpdateRolDTO
                {
                    ROL_Rol = rol.ROL_Rol,
                    ROL_Nombre = rol.ROL_Nombre,
                    ROL_Descripcion = rol.ROL_Descripcion
                }
            };
        }

        public void Crear(CreateRolDTO dto)
        {
            if (dto == null)
            {
                throw new Exception("Los datos del rol son obligatorios.");
            }

            var nombre = (dto.ROL_Nombre ?? string.Empty).Trim();
            var descripcion = string.IsNullOrWhiteSpace(dto.ROL_Descripcion) ? null : dto.ROL_Descripcion.Trim();

            if (repository.ExisteNombre(nombre))
            {
                throw new Exception("Ya existe un rol con ese nombre.");
            }

            var rol = new ACE_ROL
            {
                ROL_Nombre = nombre,
                ROL_Descripcion = descripcion,
                ROL_Estado = "A",
                ROL_Fecha_Creacion = DateTime.Now
            };

            rol = repository.Crear(rol);
            rol.ROL_Codigo = $"ROL-{rol.ROL_Rol:D6}";
            repository.Actualizar(rol);
        }

        public void Actualizar(UpdateRolDTO dto)
        {
            if (dto == null)
            {
                throw new Exception("Los datos del rol son obligatorios.");
            }

            var rol = repository.ObtenerPorId(dto.ROL_Rol);

            if (rol == null)
            {
                throw new Exception("El rol no existe.");
            }

            var nombre = (dto.ROL_Nombre ?? string.Empty).Trim();
            var descripcion = string.IsNullOrWhiteSpace(dto.ROL_Descripcion) ? null : dto.ROL_Descripcion.Trim();

            if (repository.ExisteNombre(nombre, dto.ROL_Rol))
            {
                throw new Exception("Ya existe otro rol con ese nombre.");
            }

            rol.ROL_Nombre = nombre;
            rol.ROL_Descripcion = descripcion;

            repository.Actualizar(rol);
        }

        public void EliminarLogico(int id)
        {
            var rol = repository.ObtenerPorId(id);

            if (rol == null)
            {
                throw new Exception("El rol no existe.");
            }

            rol.ROL_Estado = "I";
            repository.Actualizar(rol);
        }

        public void Activar(int id)
        {
            var rol = repository.ObtenerPorId(id);

            if (rol == null)
            {
                throw new Exception("El rol no existe.");
            }

            rol.ROL_Estado = "A";
            repository.Actualizar(rol);
        }
    }
}