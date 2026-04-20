using EncuestasAcme.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncuestasAcme.ViewModels
{
    public class LoginFormViewModel
    {
        public LoginDTO LoginDTO { get; set; }

        public LoginFormViewModel()
        {
            LoginDTO = new LoginDTO();
        }
    }
}