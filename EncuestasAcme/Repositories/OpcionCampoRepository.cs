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
    public class OpcionCampoRepository : IOpcionCampoRepository
    {
        private readonly ApplicationDbContext db;

        public OpcionCampoRepository()
        {
            db = new ApplicationDbContext();
        }

        public List<ACE_OPCION_CAMPO> ObtenerTodos()
        {
            return db.OpcionesCampo
                .Include(x => x.CampoEncuesta)
                .Where(x => x.OPC_Estado == "A")
                .OrderBy(x => x.CAM_Campo)
                .ThenBy(x => x.OPC_Orden)
                .ToList();
        }

        public List<ACE_OPCION_CAMPO> ObtenerPorCampo(int campoId)
        {
            return db.OpcionesCampo
                .Include(x => x.CampoEncuesta)
                .Where(x => x.CAM_Campo == campoId && x.OPC_Estado == "A")
                .OrderBy(x => x.OPC_Orden)
                .ToList();
        }

        public ACE_OPCION_CAMPO ObtenerPorId(int id)
        {
            return db.OpcionesCampo
                .Include(x => x.CampoEncuesta)
                .FirstOrDefault(x => x.OPC_Opcion == id);
        }

        public ACE_OPCION_CAMPO Crear(ACE_OPCION_CAMPO opcion)
        {
            db.OpcionesCampo.Add(opcion);
            db.SaveChanges();
            return opcion;
        }

        public void Actualizar(ACE_OPCION_CAMPO opcion)
        {
            db.Entry(opcion).State = EntityState.Modified;
            db.SaveChanges();
        }

        public bool ExisteTexto(int campoId, string texto, int? excluirId = null)
        {
            var query = db.OpcionesCampo.Where(x =>
                x.CAM_Campo == campoId &&
                x.OPC_Texto == texto &&
                x.OPC_Estado == "A");

            if (excluirId.HasValue)
            {
                query = query.Where(x => x.OPC_Opcion != excluirId.Value);
            }

            return query.Any();
        }

        public bool ExisteValor(int campoId, string valor, int? excluirId = null)
        {
            var query = db.OpcionesCampo.Where(x =>
                x.CAM_Campo == campoId &&
                x.OPC_Valor == valor &&
                x.OPC_Estado == "A");

            if (excluirId.HasValue)
            {
                query = query.Where(x => x.OPC_Opcion != excluirId.Value);
            }

            return query.Any();
        }

        public bool ExisteOrden(int campoId, int orden, int? excluirId = null)
        {
            var query = db.OpcionesCampo.Where(x =>
                x.CAM_Campo == campoId &&
                x.OPC_Orden == orden &&
                x.OPC_Estado == "A");

            if (excluirId.HasValue)
            {
                query = query.Where(x => x.OPC_Opcion != excluirId.Value);
            }

            return query.Any();
        }
    }
}