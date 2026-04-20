
CREATE DATABASE AcmeEncuesta;
GO

USE AcmeEncuesta;
GO

-----------
--  ROL  --
-----------
CREATE TABLE ACE_ROL(
    ROL_Rol                INT IDENTITY(1,1) NOT NULL,
    ROL_Codigo             NVARCHAR(20) NOT NULL,
    ROL_Nombre             NVARCHAR(50) NOT NULL,
    ROL_Descripcion        NVARCHAR(150) NULL,
    ROL_Estado             CHAR(1) NOT NULL CONSTRAINT DF_ACE_ROL_ESTADO DEFAULT 'A',
    ROL_Fecha_Creacion     DATETIME NOT NULL CONSTRAINT DF_ACE_ROL_FECHA DEFAULT GETDATE(),

    CONSTRAINT PK_ACE_ROL PRIMARY KEY (ROL_Rol),
    CONSTRAINT UQ_ACE_ROL_CODIGO UNIQUE (ROL_Codigo),
    CONSTRAINT UQ_ACE_ROL_NOMBRE UNIQUE (ROL_Nombre),
    CONSTRAINT CHK_ACE_ROL_ESTADO CHECK (ROL_Estado IN ('A','I'))
);
GO

---------------
--  USUARIO  --
---------------
CREATE TABLE ACE_USUARIO(
    USU_Usuario                INT IDENTITY(1,1) NOT NULL,
    USU_Codigo                 NVARCHAR(20) NOT NULL,
    USU_User_Name              NVARCHAR(50) NOT NULL,
    USU_Password_Hash          NVARCHAR(255) NOT NULL,
    USU_Primer_Nombre          NVARCHAR(25) NOT NULL,
    USU_Segundo_Nombre         NVARCHAR(25) NULL,
    USU_Primer_Apellido        NVARCHAR(25) NOT NULL,
    USU_Segundo_Apellido       NVARCHAR(25) NULL,
    USU_Correo_Electronico     NVARCHAR(150) NOT NULL,
    USU_Estado                 CHAR(1) NOT NULL CONSTRAINT DF_ACE_USUARIO_ESTADO DEFAULT 'A',
    USU_Fecha_Creacion         DATETIME NOT NULL CONSTRAINT DF_ACE_USUARIO_FECHA DEFAULT GETDATE(),
    ROL_Rol                    INT NOT NULL,

    CONSTRAINT PK_ACE_USUARIO PRIMARY KEY (USU_Usuario),
    CONSTRAINT UQ_ACE_USUARIO_CODIGO UNIQUE (USU_Codigo),
    CONSTRAINT UQ_ACE_USUARIO_USERNAME UNIQUE (USU_User_Name),
    CONSTRAINT UQ_ACE_USUARIO_CORREO UNIQUE (USU_Correo_Electronico),
    CONSTRAINT FK_ACE_USUARIO_ROL FOREIGN KEY (ROL_Rol)
        REFERENCES ACE_ROL(ROL_Rol),
    CONSTRAINT CHK_ACE_USUARIO_ESTADO CHECK (USU_Estado IN ('A','I'))
);
GO

--------------------
--  TIPO DE CAMPO --
--------------------
CREATE TABLE ACE_TIPO_CAMPO(
    TCA_Tipo_Campo             INT IDENTITY(1,1) NOT NULL,
    TCA_Codigo                 NVARCHAR(20) NOT NULL,
    TCA_Clave                  NVARCHAR(30) NOT NULL,
    TCA_Descripcion            NVARCHAR(100) NOT NULL,
    TCA_Permite_Opciones       BIT NOT NULL CONSTRAINT DF_ACE_TIPO_CAMPO_OPCIONES DEFAULT 0,
    TCA_Permite_Multiples      BIT NOT NULL CONSTRAINT DF_ACE_TIPO_CAMPO_MULTIPLES DEFAULT 0,
    TCA_Estado                 CHAR(1) NOT NULL CONSTRAINT DF_ACE_TIPO_CAMPO_ESTADO DEFAULT 'A',
    TCA_Fecha_Creacion         DATETIME NOT NULL CONSTRAINT DF_ACE_TIPO_CAMPO_FECHA DEFAULT GETDATE(),

    CONSTRAINT PK_ACE_TIPO_CAMPO PRIMARY KEY (TCA_Tipo_Campo),
    CONSTRAINT UQ_ACE_TIPO_CAMPO_CODIGO UNIQUE (TCA_Codigo),
    CONSTRAINT UQ_ACE_TIPO_CAMPO_CLAVE UNIQUE (TCA_Clave),
    CONSTRAINT CHK_ACE_TIPO_CAMPO_ESTADO CHECK (TCA_Estado IN ('A','I'))
);
GO

--------------
-- ENCUESTA --
--------------
CREATE TABLE ACE_ENCUESTA(
    ENC_Encuesta               INT IDENTITY(1,1) NOT NULL,
    ENC_Codigo                 NVARCHAR(20) NOT NULL,
    USU_Usuario                INT NOT NULL,
    ENC_Nombre                 NVARCHAR(150) NOT NULL,
    ENC_Descripcion            NVARCHAR(500) NULL,
    ENC_Token_Publico          UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_ACE_ENCUESTA_TOKEN DEFAULT NEWID(),
    ENC_Estado                 CHAR(1) NOT NULL CONSTRAINT DF_ACE_ENCUESTA_ESTADO DEFAULT 'A',
    ENC_Fecha_Creacion         DATETIME NOT NULL CONSTRAINT DF_ACE_ENCUESTA_FECHA DEFAULT GETDATE(),
    ENC_Fecha_Modificacion     DATETIME NULL,

    CONSTRAINT PK_ACE_ENCUESTA PRIMARY KEY (ENC_Encuesta),
    CONSTRAINT UQ_ACE_ENCUESTA_CODIGO UNIQUE (ENC_Codigo),
    CONSTRAINT UQ_ACE_ENCUESTA_TOKEN UNIQUE (ENC_Token_Publico),
    CONSTRAINT FK_ACE_ENCUESTA_USUARIO FOREIGN KEY (USU_Usuario)
        REFERENCES ACE_USUARIO(USU_Usuario),
    CONSTRAINT CHK_ACE_ENCUESTA_ESTADO CHECK (ENC_Estado IN ('A','I'))
);
GO

------------------------
-- CAMPO DE ENCUESTA  --
------------------------
CREATE TABLE ACE_CAMPO_ENCUESTA(
    CAM_Campo                  INT IDENTITY(1,1) NOT NULL,
    CAM_Codigo                 NVARCHAR(20) NOT NULL,
    ENC_Encuesta               INT NOT NULL,
    TCA_Tipo_Campo             INT NOT NULL,
    CAM_Nombre_Interno         NVARCHAR(100) NOT NULL,
    CAM_Titulo_Visible         NVARCHAR(150) NOT NULL,
    CAM_Es_Requerido           BIT NOT NULL CONSTRAINT DF_ACE_CAMPO_ENCUESTA_REQUERIDO DEFAULT 0,
    CAM_Orden                  INT NOT NULL,
    CAM_Estado                 CHAR(1) NOT NULL CONSTRAINT DF_ACE_CAMPO_ENCUESTA_ESTADO DEFAULT 'A',
    CAM_Fecha_Creacion         DATETIME NOT NULL CONSTRAINT DF_ACE_CAMPO_ENCUESTA_FECHA DEFAULT GETDATE(),

    CONSTRAINT PK_ACE_CAMPO_ENCUESTA PRIMARY KEY (CAM_Campo),
    CONSTRAINT UQ_ACE_CAMPO_ENCUESTA_CODIGO UNIQUE (CAM_Codigo),
    CONSTRAINT UQ_ACE_CAMPO_ENCUESTA_NOMBRE UNIQUE (ENC_Encuesta, CAM_Nombre_Interno),
    CONSTRAINT UQ_ACE_CAMPO_ENCUESTA_ORDEN UNIQUE (ENC_Encuesta, CAM_Orden),
    CONSTRAINT FK_ACE_CAMPO_ENCUESTA_ENCUESTA FOREIGN KEY (ENC_Encuesta)
        REFERENCES ACE_ENCUESTA(ENC_Encuesta),
    CONSTRAINT FK_ACE_CAMPO_ENCUESTA_TIPO FOREIGN KEY (TCA_Tipo_Campo)
        REFERENCES ACE_TIPO_CAMPO(TCA_Tipo_Campo),
    CONSTRAINT CHK_ACE_CAMPO_ENCUESTA_ESTADO CHECK (CAM_Estado IN ('A','I')),
    CONSTRAINT CHK_ACE_CAMPO_ENCUESTA_ORDEN CHECK (CAM_Orden > 0)
);
GO

---------------------
-- OPCION DE CAMPO --
---------------------
CREATE TABLE ACE_OPCION_CAMPO(
    OPC_Opcion                 INT IDENTITY(1,1) NOT NULL,
    OPC_Codigo                 NVARCHAR(20) NOT NULL,
    CAM_Campo                  INT NOT NULL,
    OPC_Texto                  NVARCHAR(200) NOT NULL,
    OPC_Valor                  NVARCHAR(100) NULL,
    OPC_Orden                  INT NOT NULL,
    OPC_Estado                 CHAR(1) NOT NULL CONSTRAINT DF_ACE_OPCION_CAMPO_ESTADO DEFAULT 'A',
    OPC_Fecha_Creacion         DATETIME NOT NULL CONSTRAINT DF_ACE_OPCION_CAMPO_FECHA DEFAULT GETDATE(),

    CONSTRAINT PK_ACE_OPCION_CAMPO PRIMARY KEY (OPC_Opcion),
    CONSTRAINT UQ_ACE_OPCION_CAMPO_CODIGO UNIQUE (OPC_Codigo),
    CONSTRAINT UQ_ACE_OPCION_CAMPO_ORDEN UNIQUE (CAM_Campo, OPC_Orden),
    CONSTRAINT FK_ACE_OPCION_CAMPO_CAMPO FOREIGN KEY (CAM_Campo)
        REFERENCES ACE_CAMPO_ENCUESTA(CAM_Campo),
    CONSTRAINT CHK_ACE_OPCION_CAMPO_ESTADO CHECK (OPC_Estado IN ('A','I')),
    CONSTRAINT CHK_ACE_OPCION_CAMPO_ORDEN CHECK (OPC_Orden > 0)
);
GO

------------------------
-- RESPUESTA CABECERA --
------------------------
CREATE TABLE ACE_RESPUESTA(
    RES_Respuesta              INT IDENTITY(1,1) NOT NULL,
    RES_Codigo                 NVARCHAR(20) NOT NULL,
    ENC_Encuesta               INT NOT NULL,
    RES_Fecha                  DATETIME NOT NULL CONSTRAINT DF_ACE_RESPUESTA_FECHA DEFAULT GETDATE(),
    RES_IP                     NVARCHAR(45) NULL,
    RES_User_Agent             NVARCHAR(300) NULL,

    CONSTRAINT PK_ACE_RESPUESTA PRIMARY KEY (RES_Respuesta),
    CONSTRAINT UQ_ACE_RESPUESTA_CODIGO UNIQUE (RES_Codigo),
    CONSTRAINT FK_ACE_RESPUESTA_ENCUESTA FOREIGN KEY (ENC_Encuesta)
        REFERENCES ACE_ENCUESTA(ENC_Encuesta)
);
GO

---------------------
-- RESPUESTA DETALLE
---------------------
CREATE TABLE ACE_RESPUESTA_DETALLE(
    RED_Detalle                INT IDENTITY(1,1) NOT NULL,
    RED_Codigo                 NVARCHAR(20) NOT NULL,
    RES_Respuesta              INT NOT NULL,
    CAM_Campo                  INT NOT NULL,
    RED_Valor_Texto            NVARCHAR(1000) NULL,
    RED_Valor_Numero           DECIMAL(18,2) NULL,
    RED_Valor_Fecha            DATE NULL,

    CONSTRAINT PK_ACE_RESPUESTA_DETALLE PRIMARY KEY (RED_Detalle),
    CONSTRAINT UQ_ACE_RESPUESTA_DETALLE_CODIGO UNIQUE (RED_Codigo),
    CONSTRAINT UQ_ACE_RESPUESTA_DETALLE_CAMPO UNIQUE (RES_Respuesta, CAM_Campo),
    CONSTRAINT FK_ACE_RESPUESTA_DETALLE_RESPUESTA FOREIGN KEY (RES_Respuesta)
        REFERENCES ACE_RESPUESTA(RES_Respuesta),
    CONSTRAINT FK_ACE_RESPUESTA_DETALLE_CAMPO FOREIGN KEY (CAM_Campo)
        REFERENCES ACE_CAMPO_ENCUESTA(CAM_Campo)
);
GO

---------------------
-- RESPUESTA OPCION --
---------------------
CREATE TABLE ACE_RESPUESTA_OPCION(
    ROP_Respuesta_Opcion       INT IDENTITY(1,1) NOT NULL,
    ROP_Codigo                 NVARCHAR(20) NOT NULL,
    RED_Detalle                INT NOT NULL,
    OPC_Opcion                 INT NOT NULL,

    CONSTRAINT PK_ACE_RESPUESTA_OPCION PRIMARY KEY (ROP_Respuesta_Opcion),
    CONSTRAINT UQ_ACE_RESPUESTA_OPCION_CODIGO UNIQUE (ROP_Codigo),
    CONSTRAINT UQ_ACE_RESPUESTA_OPCION_DETALLE_OPCION UNIQUE (RED_Detalle, OPC_Opcion),
    CONSTRAINT FK_ACE_RESPUESTA_OPCION_DETALLE FOREIGN KEY (RED_Detalle)
        REFERENCES ACE_RESPUESTA_DETALLE(RED_Detalle),
    CONSTRAINT FK_ACE_RESPUESTA_OPCION_OPCION FOREIGN KEY (OPC_Opcion)
        REFERENCES ACE_OPCION_CAMPO(OPC_Opcion)
);
GO

-------------------
-- DATOS INICIALES
-------------------

-- Roles
INSERT INTO ACE_ROL (ROL_Codigo, ROL_Nombre, ROL_Descripcion, ROL_Estado)
VALUES
('ROL-000001', 'Administrador', 'Acceso completo al sistema', 'A'),
('ROL-000002', 'Editor', 'Gestiona encuestas y respuestas', 'A'),
('ROL-000003', 'Consulta', 'Solo visualiza información', 'A');
GO

-- Usuarios de prueba
-- Contraseñas 
-- admin = Admin--
--editor = Editor--
--consulta = Consulta--
INSERT INTO ACE_USUARIO
(
    USU_Codigo,
    USU_User_Name,
    USU_Password_Hash,
    USU_Primer_Nombre,
    USU_Segundo_Nombre,
    USU_Primer_Apellido,
    USU_Segundo_Apellido,
    USU_Correo_Electronico,
    USU_Estado,
    ROL_Rol
)
VALUES
(
    'USU-000001',
    'admin',
    '$2a$11$46grrfG1brnZp7kam0n0lezWl64GmiCUdw/kNhhPliwVoHBDZ34oi',
    'Jose',
    NULL,
    'Hernandez',
    NULL,
    'admin@acme.com',
    'A',
    1
),
(
    'USU-000002',
    'editor',
    '$2a$11$SgODciMarVYALWcrF3KT..QBd35p7plyBgElEbcdLAlRuxw7B5F2y',
    'Editor',
    NULL,
    'Acme',
    NULL,
    'editor@acme.com',
    'A',
    2
),
(
    'USU-000003',
    'consulta',
    '$2a$11$DqiHtEO403BWoB7AYh9js.8ZkaW2a1RAMZsWmJpG9AK2lkYT1zIvu',
    'Consulta',
    NULL,
    'Acme',
    NULL,
    'consulta@acme.com',
    'A',
    3
);
GO

-- Tipos de campo iniciales
INSERT INTO ACE_TIPO_CAMPO
(
    TCA_Codigo,
    TCA_Clave,
    TCA_Descripcion,
    TCA_Permite_Opciones,
    TCA_Permite_Multiples,
    TCA_Estado
)
VALUES
('TCA-000001', 'TXT',  'Texto',             0, 0, 'A'),
('TCA-000002', 'NUM',  'Número',            0, 0, 'A'),
('TCA-000003', 'FEC',  'Fecha',             0, 0, 'A'),
('TCA-000004', 'SELU', 'Selección única',   1, 0, 'A'),
('TCA-000005', 'SELM', 'Selección múltiple',1, 1, 'A');
GO

-- Verificación
SELECT * FROM ACE_ROL;
SELECT * FROM ACE_USUARIO;
SELECT * FROM ACE_TIPO_CAMPO;
GO
