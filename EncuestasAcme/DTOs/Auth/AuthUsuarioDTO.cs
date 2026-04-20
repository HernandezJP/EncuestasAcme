using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncuestasAcme.DTOs.Auth
{
    public class AuthUsuarioDTO
    {
        public int USU_Usuario { get; set; }
        public string USU_Codigo { get; set; }
        public string USU_User_Name { get; set; }
        public string NombreCompleto { get; set; }
        public string USU_Correo_Electronico { get; set; }

        public int ROL_Rol { get; set; }
        public string ROL_Nombre { get; set; }
    }
}