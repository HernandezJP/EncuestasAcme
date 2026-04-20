using EncuestasAcme.DTOs.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EncuestasAcme.ViewModels
{
    public class UsuarioFormViewModel
    {
        public CreateUsuarioDTO CreateDTO { get; set; }
        public UpdateUsuarioDTO UpdateDTO { get; set; }
        public List<SelectListItem> Roles { get; set; }

        public UsuarioFormViewModel()
        {
            CreateDTO = new CreateUsuarioDTO();
            UpdateDTO = new UpdateUsuarioDTO();
            Roles = new List<SelectListItem>();
        }
    }
}