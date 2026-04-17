using EncuestasAcme.DTOs.OpcionCampo;
using EncuestasAcme.Interfaces;
using EncuestasAcme.Models;
using EncuestasAcme.Repositories;
using EncuestasAcme.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncuestasAcme.Services
{
    public class OpcionCampoService
    {
        private readonly IOpcionCampoRepository opcionRepository;
        private readonly ICampoEncuestaRepository campoRepository;
        private readonly ITipoCampoRepository tipoCampoRepository;

        public OpcionCampoService()
        {
            opcionRepository = new OpcionCampoRepository();
            campoRepository = new CampoEncuestaRepository();
            tipoCampoRepository = new TipoCampoRepository();
        }

        public List<ResponseOpcionCampoDTO> ObtenerTodos()
        {
            return opcionRepository.ObtenerTodos()
                .Select(MapearResponse)
                .ToList();
        }

        public List<ResponseOpcionCampoDTO> ObtenerPorCampo(int campoId)
        {
            return opcionRepository.ObtenerPorCampo(campoId)
                .Select(MapearResponse)
                .ToList();
        }

        public ACE_OPCION_CAMPO ObtenerPorId(int id)
        {
            return opcionRepository.ObtenerPorId(id);
        }

        public OpcionCampoFormViewModel ConstruirFormularioCrear(int campoId)
        {
            var campo = campoRepository.ObtenerPorId(campoId);

            if (campo == null)
            {
                return null;
            }

            var tipoCampo = tipoCampoRepository.ObtenerPorId(campo.TCA_Tipo_Campo);

            if (tipoCampo == null || !tipoCampo.TCA_Permite_Opciones || tipoCampo.TCA_Estado != "A")
            {
                return null;
            }

            return new OpcionCampoFormViewModel
            {
                CAM_Campo = campo.CAM_Campo,
                CAM_Titulo_Visible = campo.CAM_Titulo_Visible,
                TCA_Descripcion = tipoCampo.TCA_Descripcion,
                CreateDTO = new CreateOpcionCampoDTO
                {
                    CAM_Campo = campo.CAM_Campo,
                    OPC_Orden = 1
                }
            };
        }

        public OpcionCampoFormViewModel ConstruirFormularioEditar(int opcionId)
        {
            var opcion = opcionRepository.ObtenerPorId(opcionId);

            if (opcion == null)
            {
                return null;
            }

            var campo = campoRepository.ObtenerPorId(opcion.CAM_Campo);
            var tipoCampo = campo != null ? tipoCampoRepository.ObtenerPorId(campo.TCA_Tipo_Campo) : null;

            return new OpcionCampoFormViewModel
            {
                CAM_Campo = opcion.CAM_Campo,
                CAM_Titulo_Visible = campo != null ? campo.CAM_Titulo_Visible : string.Empty,
                TCA_Descripcion = tipoCampo != null ? tipoCampo.TCA_Descripcion : string.Empty,
                UpdateDTO = new UpdateOpcionCampoDTO
                {
                    OPC_Opcion = opcion.OPC_Opcion,
                    OPC_Texto = opcion.OPC_Texto,
                    OPC_Valor = opcion.OPC_Valor,
                    OPC_Orden = opcion.OPC_Orden
                }
            };
        }

        public void Crear(CreateOpcionCampoDTO dto)
        {
            if (dto == null)
            {
                throw new Exception("Los datos de la opción son obligatorios.");
            }

            var campo = campoRepository.ObtenerPorId(dto.CAM_Campo);
            if (campo == null)
            {
                throw new Exception("El campo no existe.");
            }

            var tipoCampo = tipoCampoRepository.ObtenerPorId(campo.TCA_Tipo_Campo);
            if (tipoCampo == null || !tipoCampo.TCA_Permite_Opciones || tipoCampo.TCA_Estado != "A")
            {
                throw new Exception("El campo seleccionado no permite opciones.");
            }

            var texto = (dto.OPC_Texto ?? string.Empty).Trim();
            var valor = (dto.OPC_Valor ?? string.Empty).Trim();

            if (opcionRepository.ExisteTexto(dto.CAM_Campo, texto))
            {
                throw new Exception("Ya existe una opción activa con ese texto para el campo.");
            }

            if (opcionRepository.ExisteValor(dto.CAM_Campo, valor))
            {
                throw new Exception("Ya existe una opción activa con ese valor para el campo.");
            }

            if (opcionRepository.ExisteOrden(dto.CAM_Campo, dto.OPC_Orden))
            {
                throw new Exception("Ya existe una opción activa con ese orden para el campo.");
            }

            var opcion = new ACE_OPCION_CAMPO
            {
                CAM_Campo = dto.CAM_Campo,
                OPC_Texto = texto,
                OPC_Valor = valor,
                OPC_Orden = dto.OPC_Orden,
                OPC_Estado = "A",
                OPC_Fecha_Creacion = DateTime.Now
            };

            opcion = opcionRepository.Crear(opcion);
            opcion.OPC_Codigo = $"OPC-{opcion.OPC_Opcion:D6}";
            opcionRepository.Actualizar(opcion);
        }

        public int Actualizar(UpdateOpcionCampoDTO dto)
        {
            if (dto == null)
            {
                throw new Exception("Los datos de la opción son obligatorios.");
            }

            var opcion = opcionRepository.ObtenerPorId(dto.OPC_Opcion);
            if (opcion == null)
            {
                throw new Exception("La opción no existe.");
            }

            var texto = (dto.OPC_Texto ?? string.Empty).Trim();
            var valor = (dto.OPC_Valor ?? string.Empty).Trim();

            if (opcionRepository.ExisteTexto(opcion.CAM_Campo, texto, dto.OPC_Opcion))
            {
                throw new Exception("Ya existe otra opción activa con ese texto para el campo.");
            }

            if (opcionRepository.ExisteValor(opcion.CAM_Campo, valor, dto.OPC_Opcion))
            {
                throw new Exception("Ya existe otra opción activa con ese valor para el campo.");
            }

            if (opcionRepository.ExisteOrden(opcion.CAM_Campo, dto.OPC_Orden, dto.OPC_Opcion))
            {
                throw new Exception("Ya existe otra opción activa con ese orden para el campo.");
            }

            opcion.OPC_Texto = texto;
            opcion.OPC_Valor = valor;
            opcion.OPC_Orden = dto.OPC_Orden;

            opcionRepository.Actualizar(opcion);

            return opcion.CAM_Campo;
        }

        public int EliminarLogico(int id)
        {
            var opcion = opcionRepository.ObtenerPorId(id);

            if (opcion == null)
            {
                throw new Exception("La opción no existe.");
            }

            opcion.OPC_Estado = "I";
            opcionRepository.Actualizar(opcion);

            return opcion.CAM_Campo;
        }

        private static ResponseOpcionCampoDTO MapearResponse(ACE_OPCION_CAMPO x)
        {
            return new ResponseOpcionCampoDTO
            {
                OPC_Opcion = x.OPC_Opcion,
                OPC_Codigo = x.OPC_Codigo,
                CAM_Campo = x.CAM_Campo,
                CAM_Titulo_Visible = x.CampoEncuesta != null ? x.CampoEncuesta.CAM_Titulo_Visible : string.Empty,
                OPC_Texto = x.OPC_Texto,
                OPC_Valor = x.OPC_Valor,
                OPC_Orden = x.OPC_Orden,
                OPC_Estado = x.OPC_Estado
            };
        }
    }
}