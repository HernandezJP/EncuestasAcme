using EncuestasAcme.Data;
using EncuestasAcme.Interfaces;
using EncuestasAcme.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EncuestasAcme.Repositories
{
    public class RespuestaOpcionRepository : IRespuestaOpcionRepository
    {
        private readonly ApplicationDbContext db;

        public RespuestaOpcionRepository()
        {
            db = new ApplicationDbContext();
        }

        public ACE_RESPUESTA_OPCION Crear(ACE_RESPUESTA_OPCION respuestaOpcion)
        {
            db.RespuestaOpciones.Add(respuestaOpcion);
            db.SaveChanges();
            return respuestaOpcion;
        }

        public List<ACE_RESPUESTA_OPCION> ObtenerPorDetalle(int detalleId)
        {
            return db.RespuestaOpciones
                .Include(x => x.OpcionCampo)
                .Where(x => x.RED_Detalle == detalleId)
                .ToList();
        }

        public void Actualizar(ACE_RESPUESTA_OPCION respuestaOpcion)
        {
            db.Entry(respuestaOpcion).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}