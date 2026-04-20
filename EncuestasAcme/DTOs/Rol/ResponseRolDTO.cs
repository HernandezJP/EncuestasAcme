using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncuestasAcme.DTOs.Rol
{
    public class ResponseRolDTO
    {
        public int ROL_Rol { get; set; }
        public string ROL_Codigo { get; set; }
        public string ROL_Nombre { get; set; }
        public string ROL_Descripcion { get; set; }
        public string ROL_Estado { get; set; }
        public DateTime ROL_Fecha_Creacion { get; set; }
    }
}