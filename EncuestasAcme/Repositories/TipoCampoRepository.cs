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
    public class TipoCampoRepository : ITipoCampoRepository
    {
        private readonly ApplicationDbContext db;

        public TipoCampoRepository()
        {
            db = new ApplicationDbContext();
        }

        public List<ACE_TIPO_CAMPO> ObtenerTodos()
        {
            return db.TiposCampo
                .OrderByDescending(x => x.TCA_Fecha_Creacion)
                .ToList();
        }

        public List<ACE_TIPO_CAMPO> ObtenerActivos()
        {
            return db.TiposCampo
                .Where(x => x.TCA_Estado == "A")
                .OrderBy(x => x.TCA_Descripcion)
                .ToList();
        }

        public ACE_TIPO_CAMPO ObtenerPorId(int id)
        {
            return db.TiposCampo.FirstOrDefault(x => x.TCA_Tipo_Campo == id);
        }

        public ACE_TIPO_CAMPO Crear(ACE_TIPO_CAMPO tipoCampo)
        {
            db.TiposCampo.Add(tipoCampo);
            db.SaveChanges();
            return tipoCampo;
        }

        public void Actualizar(ACE_TIPO_CAMPO tipoCampo)
        {
            db.Entry(tipoCampo).State = EntityState.Modified;
            db.SaveChanges();
        }

        public bool ExisteClave(string clave, int? excluirId = null)
        {
            var query = db.TiposCampo.Where(x => x.TCA_Clave == clave);

            if (excluirId.HasValue)
            {
                query = query.Where(x => x.TCA_Tipo_Campo != excluirId.Value);
            }

            return query.Any();
        }
    }
}