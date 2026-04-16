using EncuestasAcme.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncuestasAcme.Interfaces
{
    public interface ICampoEncuestaRepository
    {
        List<ACE_CAMPO_ENCUESTA> ObtenerTodos();
        List<ACE_CAMPO_ENCUESTA> ObtenerPorEncuesta(int encuestaId);
        ACE_CAMPO_ENCUESTA ObtenerPorId(int id);
        ACE_CAMPO_ENCUESTA Crear(ACE_CAMPO_ENCUESTA campo);
        void Actualizar(ACE_CAMPO_ENCUESTA campo);
        bool ExisteNombreInterno(int encuestaId, string nombreInterno, int? excluirId = null);
        bool ExisteOrden(int encuestaId, int orden, int? excluirId = null);
    }
}
