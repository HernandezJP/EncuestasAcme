using EncuestasAcme.DTOs.Rol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncuestasAcme.ViewModels
{
    public class RolFormViewModel
    {
        public CreateRolDTO CreateDTO { get; set; }
        public UpdateRolDTO UpdateDTO { get; set; }

        public RolFormViewModel()
        {
            CreateDTO = new CreateRolDTO();
            UpdateDTO = new UpdateRolDTO();
        }
    }
}