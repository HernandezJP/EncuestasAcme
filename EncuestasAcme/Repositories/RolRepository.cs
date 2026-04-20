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
    public class RolRepository : IRolRepository
    {
        private readonly ApplicationDbContext db;

        public RolRepository()
        {
            db = new ApplicationDbContext();
        }

        public List<ACE_ROL> ObtenerTodos()
        {
            return db.Roles
                .OrderBy(x => x.ROL_Nombre)
                .ToList();
        }

        public List<ACE_ROL> ObtenerActivos()
        {
            return db.Roles
                .Where(x => x.ROL_Estado == "A")
                .OrderBy(x => x.ROL_Nombre)
                .ToList();
        }

        public ACE_ROL ObtenerPorId(int id)
        {
            return db.Roles
                .FirstOrDefault(x => x.ROL_Rol == id);
        }

        public ACE_ROL Crear(ACE_ROL rol)
        {
            db.Roles.Add(rol);
            db.SaveChanges();
            return rol;
        }

        public void Actualizar(ACE_ROL rol)
        {
            db.Entry(rol).State = EntityState.Modified;
            db.SaveChanges();
        }

        public bool ExisteNombre(string nombre, int? excluirId = null)
        {
            var query = db.Roles.Where(x => x.ROL_Nombre == nombre);

            if (excluirId.HasValue)
            {
                query = query.Where(x => x.ROL_Rol != excluirId.Value);
            }

            return query.Any();
        }
    }
}