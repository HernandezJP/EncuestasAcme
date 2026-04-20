using EncuestasAcme.DTOs.Auth;
using EncuestasAcme.Helpers;
using EncuestasAcme.Interfaces;
using EncuestasAcme.Repositories;
using System;
using System.Linq;

namespace EncuestasAcme.Services
{
    public class AuthService
    {
        private readonly IUsuarioRepository usuarioRepository;

        public AuthService()
        {
            usuarioRepository = new UsuarioRepository();
        }

        public AuthUsuarioDTO Login(LoginDTO dto)
        {
            if (dto == null)
            {
                throw new Exception("Los datos de acceso son obligatorios.");
            }

            var userName = (dto.UserName ?? string.Empty).Trim();
            var password = dto.Password ?? string.Empty;

            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Usuario y contraseña son obligatorios.");
            }

            var usuario = usuarioRepository.ObtenerPorUserName(userName);

            if (usuario == null)
            {
                throw new Exception("Usuario o contraseña incorrectos.");
            }

            if (usuario.USU_Estado != "A")
            {
                throw new Exception("El usuario está inactivo.");
            }

            if (string.IsNullOrWhiteSpace(usuario.USU_Password_Hash))
            {
                throw new Exception("El usuario no tiene contraseña configurada.");
            }

            if (!PasswordHelpers.VerifyPassword(password, usuario.USU_Password_Hash))
            {
                throw new Exception("Usuario o contraseña incorrectos.");
            }

            return new AuthUsuarioDTO
            {
                USU_Usuario = usuario.USU_Usuario,
                USU_Codigo = usuario.USU_Codigo,
                USU_User_Name = usuario.USU_User_Name,
                NombreCompleto = string.Join(" ", new[]
                {
                    usuario.USU_Primer_Nombre,
                    usuario.USU_Segundo_Nombre,
                    usuario.USU_Primer_Apellido,
                    usuario.USU_Segundo_Apellido
                }.Where(x => !string.IsNullOrWhiteSpace(x))),
                USU_Correo_Electronico = usuario.USU_Correo_Electronico,
                ROL_Rol = usuario.ROL_Rol,
                ROL_Nombre = usuario.Rol != null ? usuario.Rol.ROL_Nombre : string.Empty
            };
        }
    }
}