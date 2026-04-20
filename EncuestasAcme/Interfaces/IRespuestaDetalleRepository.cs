using EncuestasAcme.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncuestasAcme.Interfaces
{
    public interface IRespuestaDetalleRepository
    {
        ACE_RESPUESTA_DETALLE Crear(ACE_RESPUESTA_DETALLE detalle);
        ACE_RESPUESTA_DETALLE ObtenerPorId(int id);
        List<ACE_RESPUESTA_DETALLE> ObtenerPorRespuesta(int respuestaId);
        void Actualizar(ACE_RESPUESTA_DETALLE detalle);
    }
}
