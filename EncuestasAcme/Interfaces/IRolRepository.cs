using EncuestasAcme.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncuestasAcme.Interfaces
{
    public interface IRolRepository
    {
        List<ACE_ROL> ObtenerTodos();
        List<ACE_ROL> ObtenerActivos();
        ACE_ROL ObtenerPorId(int id);
        ACE_ROL Crear(ACE_ROL rol);
        void Actualizar(ACE_ROL rol);
        bool ExisteNombre(string nombre, int? excluirId = null);
    }
}
