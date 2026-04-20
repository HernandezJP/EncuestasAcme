using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EncuestasAcme.Models;

namespace EncuestasAcme.Interfaces
{
    public interface IRespuestaRepository
    {
        ACE_RESPUESTA Crear(ACE_RESPUESTA respuesta);
        ACE_RESPUESTA ObtenerPorId(int id);
        List<ACE_RESPUESTA> ObtenerTodas();
        List<ACE_RESPUESTA> ObtenerPorEncuesta(int encuestaId);
        void Actualizar(ACE_RESPUESTA respuesta);
    }
}