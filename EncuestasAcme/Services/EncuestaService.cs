using EncuestasAcme.DTOs.Encuesta;
using EncuestasAcme.Interfaces;
using EncuestasAcme.Models;
using EncuestasAcme.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncuestasAcme.Services
{
    public class EncuestaService
    {
        private readonly IEncuestaRepository repository;

        public EncuestaService()
        {
            repository = new EncuestaRepository();
        }

        public List<ResponseEncuestaDTO> ObtenerTodas()
        {
            var encuestas = repository.ObtenerTodas();

            return encuestas.Select(x => new ResponseEncuestaDTO
            {
                ENC_Encuesta = x.ENC_Encuesta,
                ENC_Codigo = x.ENC_Codigo,
                ENC_Nombre = x.ENC_Nombre,
                ENC_Descripcion = x.ENC_Descripcion,
                ENC_Token_Publico = x.ENC_Token_Publico,
                ENC_Estado = x.ENC_Estado,
                ENC_Fecha_Creacion = x.ENC_Fecha_Creacion
            }).ToList();
        }

        public ACE_ENCUESTA ObtenerPorId(int id)
        {
            return repository.ObtenerPorId(id);
        }


        public DetailEncuestaDTO ObtenerDetalle(int id)
        {
            var encuesta = repository.ObtenerPorId(id);

            if (encuesta == null)
            {
                return null;
            }

            var campoService = new CampoEncuestaService();
            var campos = campoService.ObtenerPorEncuesta(id);

            return new DetailEncuestaDTO
            {
                ENC_Encuesta = encuesta.ENC_Encuesta,
                ENC_Codigo = encuesta.ENC_Codigo,
                ENC_Nombre = encuesta.ENC_Nombre,
                ENC_Descripcion = encuesta.ENC_Descripcion,
                ENC_Token_Publico = encuesta.ENC_Token_Publico,
                ENC_Estado = encuesta.ENC_Estado,
                ENC_Fecha_Creacion = encuesta.ENC_Fecha_Creacion,
                Campos = campos
            };
        }
        public void Crear(CreateEncuestaDTO dto)
        {
            if (repository.ExisteNombre(dto.ENC_Nombre))
            {
                throw new Exception("Ya existe una encuesta activa con ese nombre.");
            }

            var encuesta = new ACE_ENCUESTA
            {
                USU_Usuario = 1,
                ENC_Nombre = dto.ENC_Nombre,
                ENC_Descripcion = dto.ENC_Descripcion,
                ENC_Token_Publico = Guid.NewGuid(),
                ENC_Estado = "A",
                ENC_Fecha_Creacion = DateTime.Now
            };

            encuesta = repository.Crear(encuesta);

            encuesta.ENC_Codigo = $"ENC-{encuesta.ENC_Encuesta:D6}";

            repository.Actualizar(encuesta);
        }

        public void Actualizar(UpdateEncuestaDTO dto)
        {
            var encuesta = repository.ObtenerPorId(dto.ENC_Encuesta);

            if (encuesta == null)
            {
                throw new Exception("La encuesta no existe.");
            }

            if (repository.ExisteNombre(dto.ENC_Nombre, dto.ENC_Encuesta))
            {
                throw new Exception("Ya existe otra encuesta activa con ese nombre.");
            }

            encuesta.ENC_Nombre = dto.ENC_Nombre;
            encuesta.ENC_Descripcion = dto.ENC_Descripcion;
            encuesta.ENC_Fecha_Modificacion = DateTime.Now;

            repository.Actualizar(encuesta);
        }

        public void EliminarLogico(int id)
        {
            var encuesta = repository.ObtenerPorId(id);

            if (encuesta == null)
            {
                throw new Exception("La encuesta no existe.");
            }

            encuesta.ENC_Estado = "I";
            encuesta.ENC_Fecha_Modificacion = DateTime.Now;

            repository.Actualizar(encuesta);
        }

        public DetailEncuestaDTO ObtenerPorToken(Guid token)
        {
            var encuesta = repository.ObtenerPorToken(token);

            if (encuesta == null)
            {
                return null;
            }

            var campoService = new CampoEncuestaService();
            var campos = campoService.ObtenerPorEncuesta(encuesta.ENC_Encuesta);

            return new DetailEncuestaDTO
            {
                ENC_Encuesta = encuesta.ENC_Encuesta,
                ENC_Codigo = encuesta.ENC_Codigo,
                ENC_Nombre = encuesta.ENC_Nombre,
                ENC_Descripcion = encuesta.ENC_Descripcion,
                ENC_Token_Publico = encuesta.ENC_Token_Publico,
                ENC_Estado = encuesta.ENC_Estado,
                ENC_Fecha_Creacion = encuesta.ENC_Fecha_Creacion,
                Campos = campos
            };
        }
    }
}