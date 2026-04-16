using EncuestasAcme.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncuestasAcme.Interfaces
{
    public interface IEncuestaRepository
    {
        List<ACE_ENCUESTA> ObtenerTodas();
        ACE_ENCUESTA ObtenerPorId(int id);
        ACE_ENCUESTA Crear(ACE_ENCUESTA encuesta);
        void Actualizar(ACE_ENCUESTA encuesta);
        void Eliminar(int id);
        bool ExisteNombre(string nombre, int? excluirId = null);
    }
}
