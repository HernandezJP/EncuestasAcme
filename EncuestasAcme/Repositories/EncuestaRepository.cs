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
    public class EncuestaRepository : IEncuestaRepository
    {
        private readonly ApplicationDbContext db;

        public EncuestaRepository()
        {
            db = new ApplicationDbContext();
        }

        public List<ACE_ENCUESTA> ObtenerTodas()
        {
            return db.Encuestas
               .Where(x => x.ENC_Estado == "A")
               .OrderByDescending(x => x.ENC_Fecha_Creacion)
               .ToList();
        }

        public ACE_ENCUESTA ObtenerPorId(int id)
        {
            return db.Encuestas.FirstOrDefault(x => x.ENC_Encuesta == id && x.ENC_Estado == "A");
        }

        public ACE_ENCUESTA Crear(ACE_ENCUESTA encuesta)
        {
            db.Encuestas.Add(encuesta);
            db.SaveChanges();
            return encuesta;
        }

        public void Actualizar(ACE_ENCUESTA encuesta)
        {
            db.Entry(encuesta).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var encuesta = db.Encuestas.FirstOrDefault(x => x.ENC_Encuesta == id);

            if (encuesta != null)
            {
                encuesta.ENC_Estado = "I";
                db.SaveChanges();
            }
        }

        public bool ExisteNombre(string nombre, int? excluirId = null)
        {
            var query = db.Encuestas.Where(x => x.ENC_Nombre == nombre && x.ENC_Estado == "A");

            if (excluirId.HasValue)
            {
                query = query.Where(x => x.ENC_Encuesta != excluirId.Value);
            }

            return query.Any();
        }
    }
}