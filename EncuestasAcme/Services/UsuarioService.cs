using EncuestasAcme.DTOs.Usuario;
using EncuestasAcme.Interfaces;
using EncuestasAcme.Models;
using EncuestasAcme.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EncuestasAcme.ViewModels;
using EncuestasAcme.Helpers;

namespace EncuestasAcme.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IRolRepository rolRepository;

        public UsuarioService()
        {
            usuarioRepository = new UsuarioRepository();
            rolRepository = new RolRepository();
        }

        public List<ResponseUsuarioDTO> ObtenerTodos()
        {
            return usuarioRepository.ObtenerTodos()
                .Select(x => new ResponseUsuarioDTO
                {
                    USU_Usuario = x.USU_Usuario,
                    USU_Codigo = x.USU_Codigo,
                    USU_User_Name = x.USU_User_Name,
                    NombreCompleto = string.Join(" ", new[]
                    {
                        x.USU_Primer_Nombre,
                        x.USU_Segundo_Nombre,
                        x.USU_Primer_Apellido,
                        x.USU_Segundo_Apellido
                    }.Where(s => !string.IsNullOrWhiteSpace(s))),
                    USU_Correo_Electronico = x.USU_Correo_Electronico,
                    USU_Estado = x.USU_Estado,
                    USU_Fecha_Creacion = x.USU_Fecha_Creacion,
                    ROL_Rol = x.ROL_Rol,
                    ROL_Nombre = x.Rol != null ? x.Rol.ROL_Nombre : string.Empty
                })
                .ToList();
        }

        public DetailUsuarioDTO ObtenerDetalle(int id)
        {
            var usuario = usuarioRepository.ObtenerPorId(id);

            if (usuario == null)
            {
                return null;
            }

            return new DetailUsuarioDTO
            {
                USU_Usuario = usuario.USU_Usuario,
                USU_Codigo = usuario.USU_Codigo,
                USU_User_Name = usuario.USU_User_Name,
                USU_Primer_Nombre = usuario.USU_Primer_Nombre,
                USU_Segundo_Nombre = usuario.USU_Segundo_Nombre,
                USU_Primer_Apellido = usuario.USU_Primer_Apellido,
                USU_Segundo_Apellido = usuario.USU_Segundo_Apellido,
                NombreCompleto = string.Join(" ", new[]
             {
                usuario.USU_Primer_Nombre,
                usuario.USU_Segundo_Nombre,
                usuario.USU_Primer_Apellido,
                usuario.USU_Segundo_Apellido
            }.Where(x => !string.IsNullOrWhiteSpace(x))),
                    USU_Correo_Electronico = usuario.USU_Correo_Electronico,
                    USU_Estado = usuario.USU_Estado,
                    USU_Fecha_Creacion = usuario.USU_Fecha_Creacion,
                    ROL_Rol = usuario.ROL_Rol,
                    ROL_Codigo = usuario.Rol != null ? usuario.Rol.ROL_Codigo : string.Empty,
                    ROL_Nombre = usuario.Rol != null ? usuario.Rol.ROL_Nombre : string.Empty,
                    ROL_Descripcion = usuario.Rol != null ? usuario.Rol.ROL_Descripcion : string.Empty
             };
        }

        public ACE_USUARIO ObtenerPorId(int id)
        {
            return usuarioRepository.ObtenerPorId(id);
        }

        public UsuarioFormViewModel ConstruirFormularioCrear()
        {
            var vm = new UsuarioFormViewModel();

            vm.Roles = rolRepository.ObtenerActivos()
                .Select(x => new SelectListItem
                {
                    Value = x.ROL_Rol.ToString(),
                    Text = x.ROL_Nombre
                })
                .ToList();

            return vm;
        }

        public UsuarioFormViewModel ConstruirFormularioEditar(int usuarioId)
        {
            var usuario = usuarioRepository.ObtenerPorId(usuarioId);

            if (usuario == null)
            {
                return null;
            }

            var vm = new UsuarioFormViewModel
            {
                UpdateDTO = new UpdateUsuarioDTO
                {
                    USU_Usuario = usuario.USU_Usuario,
                    USU_User_Name = usuario.USU_User_Name,
                    USU_Primer_Nombre = usuario.USU_Primer_Nombre,
                    USU_Segundo_Nombre = usuario.USU_Segundo_Nombre,
                    USU_Primer_Apellido = usuario.USU_Primer_Apellido,
                    USU_Segundo_Apellido = usuario.USU_Segundo_Apellido,
                    USU_Correo_Electronico = usuario.USU_Correo_Electronico,
                    ROL_Rol = usuario.ROL_Rol
                }
            };

            vm.Roles = rolRepository.ObtenerActivos()
                .Select(x => new SelectListItem
                {
                    Value = x.ROL_Rol.ToString(),
                    Text = x.ROL_Nombre
                })
                .ToList();

            return vm;
        }

        public void Crear(CreateUsuarioDTO dto)
        {
            if (usuarioRepository.ExisteUsername(dto.USU_User_Name))
            {
                throw new Exception("Ya existe un usuario con ese username.");
            }

            if (usuarioRepository.ExisteCorreo(dto.USU_Correo_Electronico))
            {
                throw new Exception("Ya existe un usuario con ese correo.");
            }

            var rol = rolRepository.ObtenerPorId(dto.ROL_Rol);
            if (rol == null || rol.ROL_Estado != "A")
            {
                throw new Exception("El rol no existe o está inactivo.");
            }

            var usuario = new ACE_USUARIO
            {
                USU_User_Name = dto.USU_User_Name.Trim(),
                USU_Password_Hash = PasswordHelpers.HashPassword(dto.USU_Password_Hash),
                USU_Primer_Nombre = dto.USU_Primer_Nombre.Trim(),
                USU_Segundo_Nombre = string.IsNullOrWhiteSpace(dto.USU_Segundo_Nombre) ? null : dto.USU_Segundo_Nombre.Trim(),
                USU_Primer_Apellido = dto.USU_Primer_Apellido.Trim(),
                USU_Segundo_Apellido = string.IsNullOrWhiteSpace(dto.USU_Segundo_Apellido) ? null : dto.USU_Segundo_Apellido.Trim(),
                USU_Correo_Electronico = dto.USU_Correo_Electronico.Trim(),
                USU_Estado = "A",
                USU_Fecha_Creacion = DateTime.Now,
                ROL_Rol = dto.ROL_Rol
            };

            usuario = usuarioRepository.Crear(usuario);
            usuario.USU_Codigo = $"USU-{usuario.USU_Usuario:D6}";
            usuarioRepository.Actualizar(usuario);
        }

        public void Actualizar(UpdateUsuarioDTO dto)
        {
            var usuario = usuarioRepository.ObtenerPorId(dto.USU_Usuario);

            if (usuario == null)
            {
                throw new Exception("El usuario no existe.");
            }

            if (usuarioRepository.ExisteUsername(dto.USU_User_Name, dto.USU_Usuario))
            {
                throw new Exception("Ya existe otro usuario con ese username.");
            }

            if (usuarioRepository.ExisteCorreo(dto.USU_Correo_Electronico, dto.USU_Usuario))
            {
                throw new Exception("Ya existe otro usuario con ese correo.");
            }

            var rol = rolRepository.ObtenerPorId(dto.ROL_Rol);
            if (rol == null || rol.ROL_Estado != "A")
            {
                throw new Exception("El rol no existe o está inactivo.");
            }

            usuario.USU_User_Name = dto.USU_User_Name.Trim();
            usuario.USU_Primer_Nombre = dto.USU_Primer_Nombre.Trim();
            usuario.USU_Segundo_Nombre = string.IsNullOrWhiteSpace(dto.USU_Segundo_Nombre) ? null : dto.USU_Segundo_Nombre.Trim();
            usuario.USU_Primer_Apellido = dto.USU_Primer_Apellido.Trim();
            usuario.USU_Segundo_Apellido = string.IsNullOrWhiteSpace(dto.USU_Segundo_Apellido) ? null : dto.USU_Segundo_Apellido.Trim();
            usuario.USU_Correo_Electronico = dto.USU_Correo_Electronico.Trim();
            usuario.ROL_Rol = dto.ROL_Rol;

            if (!string.IsNullOrWhiteSpace(dto.USU_Password_Hash))
            {
                usuario.USU_Password_Hash = PasswordHelpers.HashPassword(dto.USU_Password_Hash);
            }

            usuarioRepository.Actualizar(usuario);
        }

        public void EliminarLogico(int id)
        {
            var usuario = usuarioRepository.ObtenerPorId(id);

            if (usuario == null)
            {
                throw new Exception("El usuario no existe.");
            }

            usuario.USU_Estado = "I";
            usuarioRepository.Actualizar(usuario);
        }

        public void ActivarLogico(int id)
        {
            var usuario = usuarioRepository.ObtenerPorId(id);

            if (usuario == null)
            {
                throw new Exception("El usuario no existe.");
            }

            usuario.USU_Estado = "A";
            usuarioRepository.Actualizar(usuario);
        }
    }
}