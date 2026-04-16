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
    public class CampoEncuestaRepository : ICampoEncuestaRepository
    {
        private readonly ApplicationDbContext db;

        public CampoEncuestaRepository()
        {
            db = new ApplicationDbContext();
        }

        public List<ACE_CAMPO_ENCUESTA> ObtenerTodos()
        {
            return db.CamposEncuesta
                .Include(x => x.Encuesta)
                .Include(x => x.TipoCampo)
                .Where(x => x.CAM_Estado == "A")
                .OrderBy(x => x.ENC_Encuesta)
                .ThenBy(x => x.CAM_Orden)
                .ToList();
        }

        public List<ACE_CAMPO_ENCUESTA> ObtenerPorEncuesta(int encuestaId)
        {
            return db.CamposEncuesta
                .Include(x => x.Encuesta)
                .Include(x => x.TipoCampo)
                .Where(x => x.ENC_Encuesta == encuestaId && x.CAM_Estado == "A")
                .OrderBy(x => x.CAM_Orden)
                .ToList();
        }

        public ACE_CAMPO_ENCUESTA ObtenerPorId(int id)
        {
            return db.CamposEncuesta
                .Include(x => x.Encuesta)
                .Include(x => x.TipoCampo)
                .FirstOrDefault(x => x.CAM_Campo == id);
        }

        public ACE_CAMPO_ENCUESTA Crear(ACE_CAMPO_ENCUESTA campo)
        {
            db.CamposEncuesta.Add(campo);
            db.SaveChanges();
            return campo;
        }

        public void Actualizar(ACE_CAMPO_ENCUESTA campo)
        {
            db.Entry(campo).State = EntityState.Modified;
            db.SaveChanges();
        }

        public bool ExisteNombreInterno(int encuestaId, string nombreInterno, int? excluirId = null)
        {
            var query = db.CamposEncuesta.Where(x =>
                x.ENC_Encuesta == encuestaId &&
                x.CAM_Nombre_Interno == nombreInterno &&
                x.CAM_Estado == "A");

            if (excluirId.HasValue)
            {
                query = query.Where(x => x.CAM_Campo != excluirId.Value);
            }

            return query.Any();
        }

        public bool ExisteOrden(int encuestaId, int orden, int? excluirId = null)
        {
            var query = db.CamposEncuesta.Where(x =>
                x.ENC_Encuesta == encuestaId &&
                x.CAM_Orden == orden &&
                x.CAM_Estado == "A");

            if (excluirId.HasValue)
            {
                query = query.Where(x => x.CAM_Campo != excluirId.Value);
            }

            return query.Any();
        }
    }
}