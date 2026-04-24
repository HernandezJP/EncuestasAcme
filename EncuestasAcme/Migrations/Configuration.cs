namespace EncuestasAcme.Migrations
{
    using EncuestasAcme.Helpers;
    using EncuestasAcme.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<EncuestasAcme.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(EncuestasAcme.Data.ApplicationDbContext context)
        {

            // ROLES
            if (!context.Roles.Any(x => x.ROL_Codigo == "ROL-000001"))
            {
                context.Roles.Add(new ACE_ROL
                {
                    ROL_Codigo = "ROL-000001",
                    ROL_Nombre = "Administrador",
                    ROL_Descripcion = "Acceso completo al sistema",
                    ROL_Estado = "A",
                    ROL_Fecha_Creacion = DateTime.Now
                });
            }

            if (!context.Roles.Any(x => x.ROL_Codigo == "ROL-000002"))
            {
                context.Roles.Add(new ACE_ROL
                {
                    ROL_Codigo = "ROL-000002",
                    ROL_Nombre = "Editor",
                    ROL_Descripcion = "Gestiona encuestas y respuestas",
                    ROL_Estado = "A",
                    ROL_Fecha_Creacion = DateTime.Now
                });
            }

            if (!context.Roles.Any(x => x.ROL_Codigo == "ROL-000003"))
            {
                context.Roles.Add(new ACE_ROL
                {
                    ROL_Codigo = "ROL-000003",
                    ROL_Nombre = "Consulta",
                    ROL_Descripcion = "Solo visualiza información",
                    ROL_Estado = "A",
                    ROL_Fecha_Creacion = DateTime.Now
                });
            }

            context.SaveChanges();

            var rolAdmin = context.Roles.First(x => x.ROL_Codigo == "ROL-000001").ROL_Rol;
            var rolEditor = context.Roles.First(x => x.ROL_Codigo == "ROL-000002").ROL_Rol;
            var rolConsulta = context.Roles.First(x => x.ROL_Codigo == "ROL-000003").ROL_Rol;


            //USUARIOS DE PRUEBA
            if (!context.Usuarios.Any(x => x.USU_User_Name == "admin"))
            {
                context.Usuarios.Add(new ACE_USUARIO
                {
                    USU_Codigo = "USU-000001",
                    USU_User_Name = "admin",
                    USU_Password_Hash = PasswordHelpers.HashPassword("Admin123"),
                    USU_Primer_Nombre = "Jose",
                    USU_Segundo_Nombre = "Admin",
                    USU_Primer_Apellido = "Hernandez",
                    USU_Segundo_Apellido = "",
                    USU_Correo_Electronico = "admin@acme.com",
                    USU_Estado = "A",
                    USU_Fecha_Creacion = DateTime.Now,
                    ROL_Rol = rolAdmin
                });
            }

            if (!context.Usuarios.Any(x => x.USU_User_Name == "editor"))
            {
                context.Usuarios.Add(new ACE_USUARIO
                {
                    USU_Codigo = "USU-000002",
                    USU_User_Name = "editor",
                    USU_Password_Hash = PasswordHelpers.HashPassword("Editor123"),
                    USU_Primer_Nombre = "Carlos",
                    USU_Segundo_Nombre = "",
                    USU_Primer_Apellido = "Lopez",
                    USU_Segundo_Apellido = "",
                    USU_Correo_Electronico = "editor@acme.com",
                    USU_Estado = "A",
                    USU_Fecha_Creacion = DateTime.Now,
                    ROL_Rol = rolEditor
                });
            }

            if (!context.Usuarios.Any(x => x.USU_User_Name == "consulta"))
            {
                context.Usuarios.Add(new ACE_USUARIO
                {
                    USU_Codigo = "USU-000003",
                    USU_User_Name = "consulta",
                    USU_Password_Hash = PasswordHelpers.HashPassword("Consulta123"),
                    USU_Primer_Nombre = "Ana",
                    USU_Segundo_Nombre = "",
                    USU_Primer_Apellido = "Perez",
                    USU_Segundo_Apellido = "",
                    USU_Correo_Electronico = "consulta@acme.com",
                    USU_Estado = "A",
                    USU_Fecha_Creacion = DateTime.Now,
                    ROL_Rol = rolConsulta
                });
            }

            context.SaveChanges();

            // TIPOS DE CAMPO
            if (!context.TiposCampo.Any(x => x.TCA_Clave == "TXT"))
            {
                context.TiposCampo.Add(new ACE_TIPO_CAMPO
                {
                    TCA_Codigo = "TCA-000001",
                    TCA_Clave = "TXT",
                    TCA_Descripcion = "Texto",
                    TCA_Permite_Opciones = false,
                    TCA_Permite_Multiples = false,
                    TCA_Estado = "A",
                    TCA_Fecha_Creacion = DateTime.Now
                });
            }

            if (!context.TiposCampo.Any(x => x.TCA_Clave == "NUM"))
            {
                context.TiposCampo.Add(new ACE_TIPO_CAMPO
                {
                    TCA_Codigo = "TCA-000002",
                    TCA_Clave = "NUM",
                    TCA_Descripcion = "Número",
                    TCA_Permite_Opciones = false,
                    TCA_Permite_Multiples = false,
                    TCA_Estado = "A",
                    TCA_Fecha_Creacion = DateTime.Now
                });
            }

            if (!context.TiposCampo.Any(x => x.TCA_Clave == "FEC"))
            {
                context.TiposCampo.Add(new ACE_TIPO_CAMPO
                {
                    TCA_Codigo = "TCA-000003",
                    TCA_Clave = "FEC",
                    TCA_Descripcion = "Fecha",
                    TCA_Permite_Opciones = false,
                    TCA_Permite_Multiples = false,
                    TCA_Estado = "A",
                    TCA_Fecha_Creacion = DateTime.Now
                });
            }

            if (!context.TiposCampo.Any(x => x.TCA_Clave == "SELU"))
            {
                context.TiposCampo.Add(new ACE_TIPO_CAMPO
                {
                    TCA_Codigo = "TCA-000004",
                    TCA_Clave = "SELU",
                    TCA_Descripcion = "Seleccion Única",
                    TCA_Permite_Opciones = true,
                    TCA_Permite_Multiples = false,
                    TCA_Estado = "A",
                    TCA_Fecha_Creacion = DateTime.Now
                });
            }

            if (!context.TiposCampo.Any(x => x.TCA_Clave == "SELM"))
            {
                context.TiposCampo.Add(new ACE_TIPO_CAMPO
                {
                    TCA_Codigo = "TCA-000005",
                    TCA_Clave = "SELM",
                    TCA_Descripcion = "Selección Múltiple",
                    TCA_Permite_Opciones = true,
                    TCA_Permite_Multiples = true,
                    TCA_Estado = "A",
                    TCA_Fecha_Creacion = DateTime.Now
                });
            }

            context.SaveChanges();
        }
    }
}
