using EncuestasAcme.DTOs.CampoEncuesta;
using EncuestasAcme.Interfaces;
using EncuestasAcme.Models;
using EncuestasAcme.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncuestasAcme.Services
{
    public class CampoEncuestaService
    {
        private readonly ICampoEncuestaRepository campoRepository;
        private readonly ITipoCampoRepository tipoCampoRepository;
        private readonly IEncuestaRepository encuestaRepository;

        public CampoEncuestaService()
        {
            campoRepository = new CampoEncuestaRepository();
            tipoCampoRepository = new TipoCampoRepository();
            encuestaRepository = new EncuestaRepository();
        }

        public List<ResponseCampoEncuestaDTO> ObtenerTodos()
        {
            var campos = campoRepository.ObtenerTodos();
            return campos.Select(MapearResponse).ToList();
        }

        public List<ResponseCampoEncuestaDTO> ObtenerPorEncuesta(int encuestaId)
        {
            var campos = campoRepository.ObtenerPorEncuesta(encuestaId);
            return campos.Select(MapearResponse).ToList();
        }

        public ACE_CAMPO_ENCUESTA ObtenerPorId(int id)
        {
            return campoRepository.ObtenerPorId(id);
        }

        public void Crear(CreateCampoEncuestaDTO dto)
        {
            if (dto == null)
            {
                throw new Exception("Los datos del campo son obligatorios.");
            }

            var encuesta = encuestaRepository.ObtenerPorId(dto.ENC_Encuesta);
            if (encuesta == null)
            {
                throw new Exception("La encuesta no existe.");
            }

            var tipoCampo = tipoCampoRepository.ObtenerPorId(dto.TCA_Tipo_Campo);
            if (tipoCampo == null || tipoCampo.TCA_Estado != "A")
            {
                throw new Exception("El tipo de campo no existe o está inactivo.");
            }

            var nombreInterno = (dto.CAM_Nombre_Interno ?? string.Empty).Trim();
            var tituloVisible = (dto.CAM_Titulo_Visible ?? string.Empty).Trim();

            if (campoRepository.ExisteNombreInterno(dto.ENC_Encuesta, nombreInterno))
            {
                throw new Exception("Ya existe un campo activo con ese nombre interno en la encuesta.");
            }

            if (campoRepository.ExisteOrden(dto.ENC_Encuesta, dto.CAM_Orden))
            {
                throw new Exception("Ya existe un campo activo con ese orden en la encuesta.");
            }

            var campo = new ACE_CAMPO_ENCUESTA
            {
                ENC_Encuesta = dto.ENC_Encuesta,
                TCA_Tipo_Campo = dto.TCA_Tipo_Campo,
                CAM_Nombre_Interno = nombreInterno,
                CAM_Titulo_Visible = tituloVisible,
                CAM_Es_Requerido = dto.CAM_Es_Requerido,
                CAM_Orden = dto.CAM_Orden,
                CAM_Estado = "A",
                CAM_Fecha_Creacion = DateTime.Now
            };

            campo = campoRepository.Crear(campo);

            campo.CAM_Codigo = $"CAM-{campo.CAM_Campo:D6}";
            campoRepository.Actualizar(campo);
        }

        public int Actualizar(UpdateCampoEncuestaDTO dto)
        {
            if (dto == null)
            {
                throw new Exception("Los datos del campo son obligatorios.");
            }

            var campo = campoRepository.ObtenerPorId(dto.CAM_Campo);
            if (campo == null)
            {
                throw new Exception("El campo no existe.");
            }

            var tipoCampo = tipoCampoRepository.ObtenerPorId(dto.TCA_Tipo_Campo);
            if (tipoCampo == null || tipoCampo.TCA_Estado != "A")
            {
                throw new Exception("El tipo de campo no existe o está inactivo.");
            }

            var nombreInterno = (dto.CAM_Nombre_Interno ?? string.Empty).Trim();
            var tituloVisible = (dto.CAM_Titulo_Visible ?? string.Empty).Trim();

            if (campoRepository.ExisteNombreInterno(campo.ENC_Encuesta, nombreInterno, dto.CAM_Campo))
            {
                throw new Exception("Ya existe otro campo activo con ese nombre interno en la encuesta.");
            }

            if (campoRepository.ExisteOrden(campo.ENC_Encuesta, dto.CAM_Orden, dto.CAM_Campo))
            {
                throw new Exception("Ya existe otro campo activo con ese orden en la encuesta.");
            }

            campo.TCA_Tipo_Campo = dto.TCA_Tipo_Campo;
            campo.CAM_Nombre_Interno = nombreInterno;
            campo.CAM_Titulo_Visible = tituloVisible;
            campo.CAM_Es_Requerido = dto.CAM_Es_Requerido;
            campo.CAM_Orden = dto.CAM_Orden;

            campoRepository.Actualizar(campo);

            return campo.ENC_Encuesta;
        }

        public int EliminarLogico(int id)
        {
            var campo = campoRepository.ObtenerPorId(id);

            if (campo == null)
            {
                throw new Exception("El campo no existe.");
            }

            campo.CAM_Estado = "I";
            campoRepository.Actualizar(campo);

            return campo.ENC_Encuesta;
        }

        private static ResponseCampoEncuestaDTO MapearResponse(ACE_CAMPO_ENCUESTA x)
        {
            return new ResponseCampoEncuestaDTO
            {
                CAM_Campo = x.CAM_Campo,
                CAM_Codigo = x.CAM_Codigo,
                ENC_Encuesta = x.ENC_Encuesta,
                ENC_Nombre = x.Encuesta != null ? x.Encuesta.ENC_Nombre : string.Empty,
                TCA_Tipo_Campo = x.TCA_Tipo_Campo,
                TCA_Descripcion = x.TipoCampo != null ? x.TipoCampo.TCA_Descripcion : string.Empty,
                TCA_Permite_Opciones = x.TipoCampo != null && x.TipoCampo.TCA_Permite_Opciones,
                TCA_Permite_Multiples = x.TipoCampo != null && x.TipoCampo.TCA_Permite_Multiples,
                CAM_Nombre_Interno = x.CAM_Nombre_Interno,
                CAM_Titulo_Visible = x.CAM_Titulo_Visible,
                CAM_Es_Requerido = x.CAM_Es_Requerido,
                CAM_Orden = x.CAM_Orden,
                CAM_Estado = x.CAM_Estado
            };
        }
    }
}