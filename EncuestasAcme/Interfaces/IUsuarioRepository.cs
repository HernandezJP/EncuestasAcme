using EncuestasAcme.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncuestasAcme.Interfaces
{
    public interface IUsuarioRepository
    {
        List<ACE_USUARIO> ObtenerTodos();
        ACE_USUARIO ObtenerPorId(int id);
        ACE_USUARIO ObtenerPorUserName(string userName);
        ACE_USUARIO Crear(ACE_USUARIO usuario);
        void Actualizar(ACE_USUARIO usuario);
        bool ExisteUsername(string username, int? excluirId = null);
        bool ExisteCorreo(string correo, int? excluirId = null);
    }
}
