using EncuestasAcme.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncuestasAcme.Interfaces
{
    public interface IRespuestaOpcionRepository
    {
        ACE_RESPUESTA_OPCION Crear(ACE_RESPUESTA_OPCION respuestaOpcion);
        List<ACE_RESPUESTA_OPCION> ObtenerPorDetalle(int detalleId);
        void Actualizar(ACE_RESPUESTA_OPCION respuestaOpcion);
    }
}
