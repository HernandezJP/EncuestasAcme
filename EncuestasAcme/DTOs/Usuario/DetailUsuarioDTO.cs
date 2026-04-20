using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncuestasAcme.DTOs.Usuario
{
    public class DetailUsuarioDTO
    {
        public int USU_Usuario { get; set; }
        public string USU_Codigo { get; set; }
        public string USU_User_Name { get; set; }
        public string USU_Primer_Nombre { get; set; }
        public string USU_Segundo_Nombre { get; set; }
        public string USU_Primer_Apellido { get; set; }
        public string USU_Segundo_Apellido { get; set; }
        public string NombreCompleto { get; set; }
        public string USU_Correo_Electronico { get; set; }
        public string USU_Estado { get; set; }
        public DateTime USU_Fecha_Creacion { get; set; }

        public int ROL_Rol { get; set; }
        public string ROL_Codigo { get; set; }
        public string ROL_Nombre { get; set; }
        public string ROL_Descripcion { get; set; }
    }
}