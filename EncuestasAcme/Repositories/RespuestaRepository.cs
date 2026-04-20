using EncuestasAcme.Data;
using EncuestasAcme.Models;
using EncuestasAcme.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EncuestasAcme.Repositories
{
    public class RespuestaRepository : IRespuestaRepository
    {
        private readonly ApplicationDbContext db;

        public RespuestaRepository()
        {
            db = new ApplicationDbContext();
        }

        public ACE_RESPUESTA Crear(ACE_RESPUESTA respuesta)
        {
            db.Respuestas.Add(respuesta);
            db.SaveChanges();
            return respuesta;
        }

        public ACE_RESPUESTA ObtenerPorId(int id)
        {
            return db.Respuestas
                .Include(x => x.Encuesta)
                .FirstOrDefault(x => x.RES_Respuesta == id);
        }

        public List<ACE_RESPUESTA> ObtenerTodas()
        {
            return db.Respuestas
                .Include(x => x.Encuesta)
                .OrderByDescending(x => x.RES_Fecha)
                .ToList();
        }

        public List<ACE_RESPUESTA> ObtenerPorEncuesta(int encuestaId)
        {
            return db.Respuestas
                .Include(x => x.Encuesta)
                .Where(x => x.ENC_Encuesta == encuestaId)
                .OrderByDescending(x => x.RES_Fecha)
                .ToList();
        }

        public void Actualizar(ACE_RESPUESTA respuesta)
        {
            db.Entry(respuesta).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}