using EncuestasAcme.DTOs.Respuesta;
using EncuestasAcme.DTOs.RespuestaDetalle;
using EncuestasAcme.Interfaces;
using EncuestasAcme.Models;
using EncuestasAcme.Repositories;
using EncuestasAcme.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EncuestasAcme.Services
{
    public class RespuestaService
    {
        private readonly IRespuestaRepository respuestaRepository;
        private readonly IRespuestaDetalleRepository detalleRepository;

        public RespuestaService()
        {
            respuestaRepository = new RespuestaRepository();
            detalleRepository = new RespuestaDetalleRepository();
        }

        public List<ResponseRespuestaDTO> ObtenerTodas()
        {
            return respuestaRepository.ObtenerTodas()
                .Select(x => new ResponseRespuestaDTO
                {
                    RES_Respuesta = x.RES_Respuesta,
                    RES_Codigo = x.RES_Codigo,
                    ENC_Encuesta = x.ENC_Encuesta,
                    ENC_Nombre = x.Encuesta != null ? x.Encuesta.ENC_Nombre : string.Empty,
                    RES_Fecha = x.RES_Fecha,
                    RES_IP = x.RES_IP
                })
                .ToList();
        }

        public ResponseRespuestaCompletaDTO ObtenerDetalle(int respuestaId)
        {
            var respuesta = respuestaRepository.ObtenerPorId(respuestaId);

            if (respuesta == null)
            {
                return null;
            }

            var detalles = detalleRepository.ObtenerPorRespuesta(respuestaId);
            var opcionRepository = new RespuestaOpcionRepository();

            return new ResponseRespuestaCompletaDTO
            {
                RES_Respuesta = respuesta.RES_Respuesta,
                RES_Codigo = respuesta.RES_Codigo,
                ENC_Encuesta = respuesta.ENC_Encuesta,
                ENC_Nombre = respuesta.Encuesta != null ? respuesta.Encuesta.ENC_Nombre : string.Empty,
                RES_Fecha = respuesta.RES_Fecha,
                RES_IP = respuesta.RES_IP,
                RES_User_Agent = respuesta.RES_User_Agent,
                Detalles = detalles.Select(x =>
                {
                    var opciones = opcionRepository.ObtenerPorDetalle(x.RED_Detalle);

                    return new ResponseRespuestaDetalleDTO
                    {
                        RED_Detalle = x.RED_Detalle,
                        RED_Codigo = x.RED_Codigo,
                        RES_Respuesta = x.RES_Respuesta,
                        CAM_Campo = x.CAM_Campo,
                        CAM_Titulo_Visible = x.CampoEncuesta?.CAM_Titulo_Visible,
                        TCA_Descripcion = x.CampoEncuesta?.TipoCampo?.TCA_Descripcion,

                        RED_Valor_Texto = x.RED_Valor_Texto,
                        RED_Valor_Numero = x.RED_Valor_Numero,
                        RED_Valor_Fecha = x.RED_Valor_Fecha,

                        OpcionesSeleccionadas = opciones != null && opciones.Any()
                            ? string.Join(", ", opciones.Select(o => o.OpcionCampo.OPC_Texto))
                            : ""
                    };
                }).ToList()
            };
        }

    }
}