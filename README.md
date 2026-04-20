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
-El script necesario es el que lleva por nombre AcmeEcnuestas.sql
