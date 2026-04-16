using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncuestasAcme.Models
{
    public class ACE_USUARIO
    {
        public int USU_Usuario { get; set; }
        public string USU_Codigo { get; set; }
        public string USU_User_Name { get; set; }
        public string USU_Password_Hash { get; set; }
        public string USU_Primer_Nombre { get; set; }
        public string USU_Segundo_Nombre { get; set; }
        public string USU_Primer_Apellido { get; set; }
        public string USU_Segundo_Apellido { get; set; }
        public string USU_Correo_Electronico { get; set; }
        public string USU_Estado { get; set; }
        public DateTime USU_Fecha_Creacion { get; set; }
    }
}