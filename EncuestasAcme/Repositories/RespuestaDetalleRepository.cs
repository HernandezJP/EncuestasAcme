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
    public class RespuestaDetalleRepository : IRespuestaDetalleRepository
    {
        private readonly ApplicationDbContext db;

        public RespuestaDetalleRepository()
        {
            db = new ApplicationDbContext();
        }

        public ACE_RESPUESTA_DETALLE Crear(ACE_RESPUESTA_DETALLE detalle)
        {
            db.RespuestaDetalles.Add(detalle);
            db.SaveChanges();
            return detalle;
        }

        public ACE_RESPUESTA_DETALLE ObtenerPorId(int id)
        {
            return db.RespuestaDetalles
                .Include(x => x.Respuesta)
                .Include(x => x.CampoEncuesta)
                .Include(x => x.CampoEncuesta.TipoCampo)
                .FirstOrDefault(x => x.RED_Detalle == id);
        }

        public List<ACE_RESPUESTA_DETALLE> ObtenerPorRespuesta(int respuestaId)
        {
            return db.RespuestaDetalles
                .Include(x => x.Respuesta)
                .Include(x => x.CampoEncuesta)
                .Include(x => x.CampoEncuesta.TipoCampo)
                .Where(x => x.RES_Respuesta == respuestaId)
                .OrderBy(x => x.CampoEncuesta.CAM_Orden)
                .ToList();
        }

        public void Actualizar(ACE_RESPUESTA_DETALLE detalle)
        {
            db.Entry(detalle).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}