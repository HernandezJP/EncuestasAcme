using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EncuestasAcme.Models;

namespace EncuestasAcme.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }
        public DbSet<ACE_USUARIO> Usuarios { get; set; }
        public DbSet<ACE_ROL> Roles { get; set; }
        public DbSet<ACE_TIPO_CAMPO> TiposCampo { get; set; }
        public DbSet<ACE_ENCUESTA> Encuestas { get; set; }
        public DbSet<ACE_CAMPO_ENCUESTA> CamposEncuesta { get; set; }
        public DbSet<ACE_OPCION_CAMPO> OpcionesCampo { get; set; }
        public DbSet<ACE_RESPUESTA> Respuestas { get; set; }
        public DbSet<ACE_RESPUESTA_DETALLE> RespuestaDetalles { get; set; }
        public DbSet<ACE_RESPUESTA_OPCION> RespuestaOpciones { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ACE_USUARIO>().ToTable("ACE_USUARIO").HasKey(x => x.USU_Usuario);
            modelBuilder.Entity<ACE_ROL>().ToTable("ACE_ROL").HasKey(x => x.ROL_Rol);
            modelBuilder.Entity<ACE_TIPO_CAMPO>().ToTable("ACE_TIPO_CAMPO").HasKey(x => x.TCA_Tipo_Campo);
            modelBuilder.Entity<ACE_ENCUESTA>().ToTable("ACE_ENCUESTA").HasKey(x => x.ENC_Encuesta);
            modelBuilder.Entity<ACE_CAMPO_ENCUESTA>().ToTable("ACE_CAMPO_ENCUESTA").HasKey(x => x.CAM_Campo);
            modelBuilder.Entity<ACE_OPCION_CAMPO>().ToTable("ACE_OPCION_CAMPO").HasKey(x => x.OPC_Opcion);
            modelBuilder.Entity<ACE_RESPUESTA>().ToTable("ACE_RESPUESTA").HasKey(x => x.RES_Respuesta);
            modelBuilder.Entity<ACE_RESPUESTA_DETALLE>().ToTable("ACE_RESPUESTA_DETALLE").HasKey(x => x.RED_Detalle);
            modelBuilder.Entity<ACE_RESPUESTA_OPCION>().ToTable("ACE_RESPUESTA_OPCION").HasKey(x => x.ROP_Respuesta_Opcion);
        }
    }
}