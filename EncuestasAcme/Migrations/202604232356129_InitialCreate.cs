namespace EncuestasAcme.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ACE_CAMPO_ENCUESTA",
                c => new
                    {
                        CAM_Campo = c.Int(nullable: false, identity: true),
                        CAM_Codigo = c.String(),
                        ENC_Encuesta = c.Int(nullable: false),
                        TCA_Tipo_Campo = c.Int(nullable: false),
                        CAM_Nombre_Interno = c.String(),
                        CAM_Titulo_Visible = c.String(),
                        CAM_Es_Requerido = c.Boolean(nullable: false),
                        CAM_Orden = c.Int(nullable: false),
                        CAM_Estado = c.String(),
                        CAM_Fecha_Creacion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CAM_Campo)
                .ForeignKey("dbo.ACE_ENCUESTA", t => t.ENC_Encuesta)
                .ForeignKey("dbo.ACE_TIPO_CAMPO", t => t.TCA_Tipo_Campo)
                .Index(t => t.ENC_Encuesta)
                .Index(t => t.TCA_Tipo_Campo);
            
            CreateTable(
                "dbo.ACE_ENCUESTA",
                c => new
                    {
                        ENC_Encuesta = c.Int(nullable: false, identity: true),
                        ENC_Codigo = c.String(),
                        USU_Usuario = c.Int(nullable: false),
                        ENC_Nombre = c.String(),
                        ENC_Descripcion = c.String(),
                        ENC_Token_Publico = c.Guid(nullable: false),
                        ENC_Estado = c.String(),
                        ENC_Fecha_Creacion = c.DateTime(nullable: false),
                        ENC_Fecha_Modificacion = c.DateTime(),
                    })
                .PrimaryKey(t => t.ENC_Encuesta);
            
            CreateTable(
                "dbo.ACE_TIPO_CAMPO",
                c => new
                    {
                        TCA_Tipo_Campo = c.Int(nullable: false, identity: true),
                        TCA_Codigo = c.String(),
                        TCA_Clave = c.String(),
                        TCA_Descripcion = c.String(),
                        TCA_Permite_Opciones = c.Boolean(nullable: false),
                        TCA_Permite_Multiples = c.Boolean(nullable: false),
                        TCA_Estado = c.String(),
                        TCA_Fecha_Creacion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TCA_Tipo_Campo);
            
            CreateTable(
                "dbo.ACE_OPCION_CAMPO",
                c => new
                    {
                        OPC_Opcion = c.Int(nullable: false, identity: true),
                        OPC_Codigo = c.String(),
                        CAM_Campo = c.Int(nullable: false),
                        OPC_Texto = c.String(),
                        OPC_Valor = c.String(),
                        OPC_Orden = c.Int(nullable: false),
                        OPC_Estado = c.String(),
                        OPC_Fecha_Creacion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OPC_Opcion)
                .ForeignKey("dbo.ACE_CAMPO_ENCUESTA", t => t.CAM_Campo)
                .Index(t => t.CAM_Campo);
            
            CreateTable(
                "dbo.ACE_RESPUESTA_DETALLE",
                c => new
                    {
                        RED_Detalle = c.Int(nullable: false, identity: true),
                        RED_Codigo = c.String(),
                        RES_Respuesta = c.Int(nullable: false),
                        CAM_Campo = c.Int(nullable: false),
                        RED_Valor_Texto = c.String(),
                        RED_Valor_Numero = c.Decimal(precision: 18, scale: 2),
                        RED_Valor_Fecha = c.DateTime(),
                    })
                .PrimaryKey(t => t.RED_Detalle)
                .ForeignKey("dbo.ACE_CAMPO_ENCUESTA", t => t.CAM_Campo)
                .ForeignKey("dbo.ACE_RESPUESTA", t => t.RES_Respuesta)
                .Index(t => t.RES_Respuesta)
                .Index(t => t.CAM_Campo);
            
            CreateTable(
                "dbo.ACE_RESPUESTA",
                c => new
                    {
                        RES_Respuesta = c.Int(nullable: false, identity: true),
                        RES_Codigo = c.String(),
                        ENC_Encuesta = c.Int(nullable: false),
                        RES_Fecha = c.DateTime(nullable: false),
                        RES_IP = c.String(),
                        RES_User_Agent = c.String(),
                    })
                .PrimaryKey(t => t.RES_Respuesta)
                .ForeignKey("dbo.ACE_ENCUESTA", t => t.ENC_Encuesta)
                .Index(t => t.ENC_Encuesta);
            
            CreateTable(
                "dbo.ACE_RESPUESTA_OPCION",
                c => new
                    {
                        ROP_Respuesta_Opcion = c.Int(nullable: false, identity: true),
                        ROP_Codigo = c.String(),
                        RED_Detalle = c.Int(nullable: false),
                        OPC_Opcion = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ROP_Respuesta_Opcion)
                .ForeignKey("dbo.ACE_OPCION_CAMPO", t => t.OPC_Opcion)
                .ForeignKey("dbo.ACE_RESPUESTA_DETALLE", t => t.RED_Detalle)
                .Index(t => t.RED_Detalle)
                .Index(t => t.OPC_Opcion);
            
            CreateTable(
                "dbo.ACE_ROL",
                c => new
                    {
                        ROL_Rol = c.Int(nullable: false, identity: true),
                        ROL_Codigo = c.String(),
                        ROL_Nombre = c.String(),
                        ROL_Descripcion = c.String(),
                        ROL_Estado = c.String(),
                        ROL_Fecha_Creacion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ROL_Rol);
            
            CreateTable(
                "dbo.ACE_USUARIO",
                c => new
                    {
                        USU_Usuario = c.Int(nullable: false, identity: true),
                        USU_Codigo = c.String(),
                        USU_User_Name = c.String(),
                        USU_Password_Hash = c.String(),
                        USU_Primer_Nombre = c.String(),
                        USU_Segundo_Nombre = c.String(),
                        USU_Primer_Apellido = c.String(),
                        USU_Segundo_Apellido = c.String(),
                        USU_Correo_Electronico = c.String(),
                        USU_Estado = c.String(),
                        USU_Fecha_Creacion = c.DateTime(nullable: false),
                        ROL_Rol = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.USU_Usuario)
                .ForeignKey("dbo.ACE_ROL", t => t.ROL_Rol)
                .Index(t => t.ROL_Rol);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ACE_USUARIO", "ROL_Rol", "dbo.ACE_ROL");
            DropForeignKey("dbo.ACE_RESPUESTA_OPCION", "RED_Detalle", "dbo.ACE_RESPUESTA_DETALLE");
            DropForeignKey("dbo.ACE_RESPUESTA_OPCION", "OPC_Opcion", "dbo.ACE_OPCION_CAMPO");
            DropForeignKey("dbo.ACE_RESPUESTA_DETALLE", "RES_Respuesta", "dbo.ACE_RESPUESTA");
            DropForeignKey("dbo.ACE_RESPUESTA", "ENC_Encuesta", "dbo.ACE_ENCUESTA");
            DropForeignKey("dbo.ACE_RESPUESTA_DETALLE", "CAM_Campo", "dbo.ACE_CAMPO_ENCUESTA");
            DropForeignKey("dbo.ACE_OPCION_CAMPO", "CAM_Campo", "dbo.ACE_CAMPO_ENCUESTA");
            DropForeignKey("dbo.ACE_CAMPO_ENCUESTA", "TCA_Tipo_Campo", "dbo.ACE_TIPO_CAMPO");
            DropForeignKey("dbo.ACE_CAMPO_ENCUESTA", "ENC_Encuesta", "dbo.ACE_ENCUESTA");
            DropIndex("dbo.ACE_USUARIO", new[] { "ROL_Rol" });
            DropIndex("dbo.ACE_RESPUESTA_OPCION", new[] { "OPC_Opcion" });
            DropIndex("dbo.ACE_RESPUESTA_OPCION", new[] { "RED_Detalle" });
            DropIndex("dbo.ACE_RESPUESTA", new[] { "ENC_Encuesta" });
            DropIndex("dbo.ACE_RESPUESTA_DETALLE", new[] { "CAM_Campo" });
            DropIndex("dbo.ACE_RESPUESTA_DETALLE", new[] { "RES_Respuesta" });
            DropIndex("dbo.ACE_OPCION_CAMPO", new[] { "CAM_Campo" });
            DropIndex("dbo.ACE_CAMPO_ENCUESTA", new[] { "TCA_Tipo_Campo" });
            DropIndex("dbo.ACE_CAMPO_ENCUESTA", new[] { "ENC_Encuesta" });
            DropTable("dbo.ACE_USUARIO");
            DropTable("dbo.ACE_ROL");
            DropTable("dbo.ACE_RESPUESTA_OPCION");
            DropTable("dbo.ACE_RESPUESTA");
            DropTable("dbo.ACE_RESPUESTA_DETALLE");
            DropTable("dbo.ACE_OPCION_CAMPO");
            DropTable("dbo.ACE_TIPO_CAMPO");
            DropTable("dbo.ACE_ENCUESTA");
            DropTable("dbo.ACE_CAMPO_ENCUESTA");
        }
    }
}
