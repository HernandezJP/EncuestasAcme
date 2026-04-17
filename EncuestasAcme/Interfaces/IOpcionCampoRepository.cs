using EncuestasAcme.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncuestasAcme.Interfaces
{
    public interface IOpcionCampoRepository
    {
        List<ACE_OPCION_CAMPO> ObtenerTodos();
        List<ACE_OPCION_CAMPO> ObtenerPorCampo(int campoId);
        ACE_OPCION_CAMPO ObtenerPorId(int id);
        ACE_OPCION_CAMPO Crear(ACE_OPCION_CAMPO opcion);
        void Actualizar(ACE_OPCION_CAMPO opcion);
        bool ExisteTexto(int campoId, string texto, int? excluirId = null);
        bool ExisteValor(int campoId, string valor, int? excluirId = null);
        bool ExisteOrden(int campoId, int orden, int? excluirId = null);
    }
}
