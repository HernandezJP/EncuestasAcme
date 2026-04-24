# AcmeEncuestas

Sistema web para la administración y publicación de encuestas desarrollado en ASP.NET MVC.


## 📌 Descripción

AcmeEncuestas permite crear encuestas dinámicas con distintos tipos de preguntas, responderlas desde el sistema o mediante un enlace público y administrar usuarios con roles.

---

## 🛠 Tecnologías utilizadas

- ASP.NET MVC 5
- C#
- Entity Framework
- SQL Server
- Razor Views
- BCrypt.Net (hash de contraseñas)
- FormsAuthentication

---

## ⚙️ Funcionalidades

- Login con control de acceso por roles
- Gestión de usuarios
- Gestión de roles
- Creación de encuestas
- Campos dinámicos por encuesta
- Opciones para preguntas
- Respuestas internas
- Respuestas públicas mediante token
- Dashboard básico

---

## 🔐 Seguridad

- Contraseñas protegidas con BCrypt
- Verificación segura de credenciales
- Autenticación con sesión (FormsAuthentication)
- Control de acceso por roles
- Token público con GUID para encuestas

---

## 🗄 Base de datos

El proyecto utiliza **Entity Framework con migraciones** para la gestión de la base de datos.

### 🔄 Creación automática

Al ejecutar la aplicación por primera vez:

- Entity Framework valida si la base de datos `EncuestasAcme` existe en la instancia configurada (`.\SQLEXPRESS`)
- Si no existe, se crea automáticamente
- Se ejecutan las migraciones definidas en el proyecto (`Migrations`)
- Se construye toda la estructura de tablas, relaciones y constraints

### ⚙️ Migraciones

Las migraciones permiten versionar la base de datos a partir del modelo en código:

- Cada cambio en las entidades genera una migración
- Las migraciones se aplican automáticamente al iniciar la aplicación
- No es necesario ejecutar comandos manuales como `Update-Database`

### 🌱 Datos iniciales (Seed)

Después de aplicar las migraciones, se ejecuta el método `Seed()` que:

- Inserta roles iniciales (Administrador, Editor, Consulta)
- Crea usuarios de prueba con contraseñas encriptadas (BCrypt)
- Registra catálogos base (tipos de campo, configuraciones iniciales)

Este proceso evita duplicados mediante validaciones

### ❌ Script SQL

No es necesario ejecutar manualmente el script:

- `AcmeEncuestas.sql`

### 📌 Nota

El archivo `AcmeEncuestas.sql` se mantiene únicamente como:

- Referencia de la estructura de base de datos
- Respaldo manual en caso de ser requerido

---
