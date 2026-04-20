using EncuestasAcme.Data;
using EncuestasAcme.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EncuestasAcme.Interfaces;

namespace EncuestasAcme.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    { 
        private readonly ApplicationDbContext db;

        public UsuarioRepository()
        {
            db = new ApplicationDbContext();
        }

        public List<ACE_USUARIO> ObtenerTodos()
        {
            return db.Usuarios
                .Include(x => x.Rol)
                .OrderByDescending(x => x.USU_Fecha_Creacion)
                .ToList();
        }

        public ACE_USUARIO ObtenerPorId(int id)
        {
            return db.Usuarios
                .Include(x => x.Rol)
                .FirstOrDefault(x => x.USU_Usuario == id);
        }

        public ACE_USUARIO ObtenerPorUserName(string userName)
        {
            return db.Usuarios
                .Include(x => x.Rol)
                .FirstOrDefault(x => x.USU_User_Name == userName);
        }

        public ACE_USUARIO Crear(ACE_USUARIO usuario)
        {
            db.Usuarios.Add(usuario);
            db.SaveChanges();
            return usuario;
        }

        public void Actualizar(ACE_USUARIO usuario)
        {
            db.Entry(usuario).State = EntityState.Modified;
            db.SaveChanges();
        }

        public bool ExisteUsername(string username, int? excluirId = null)
        {
            var query = db.Usuarios.Where(x => x.USU_User_Name == username);

            if (excluirId.HasValue)
            {
                query = query.Where(x => x.USU_Usuario != excluirId.Value);
            }

            return query.Any();
        }

        public bool ExisteCorreo(string correo, int? excluirId = null)
        {
            var query = db.Usuarios.Where(x => x.USU_Correo_Electronico == correo);

            if (excluirId.HasValue)
            {
                query = query.Where(x => x.USU_Usuario != excluirId.Value);
            }

            return query.Any();
        }
    }
}