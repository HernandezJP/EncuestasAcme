using EncuestasAcme.DTOs.Encuesta;
using EncuestasAcme.Interfaces;
using EncuestasAcme.Models;
using EncuestasAcme.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace EncuestasAcme.Services
{
    public class EncuestaRespuestaInternaService
    {
        private readonly IRespuestaRepository respuestaRepository;
        private readonly IRespuestaDetalleRepository detalleRepository;
        private readonly IRespuestaOpcionRepository respuestaOpcionRepository;
        private readonly ICampoEncuestaRepository campoRepository;

        public EncuestaRespuestaInternaService()
        {
            respuestaRepository = new RespuestaRepository();
            detalleRepository = new RespuestaDetalleRepository();
            respuestaOpcionRepository = new RespuestaOpcionRepository();
            campoRepository = new CampoEncuestaRepository();
        }

        public void GuardarRespuesta(ResponderEncuestaDTO dto, string ip, string userAgent)
        {
            var respuesta = new ACE_RESPUESTA
            {
                ENC_Encuesta = dto.ENC_Encuesta,
                RES_Fecha = DateTime.Now,
                RES_IP = ip,
                RES_User_Agent = userAgent
            };

            respuesta = respuestaRepository.Crear(respuesta);
            respuesta.RES_Codigo = $"RES-{respuesta.RES_Respuesta:D6}";
            respuestaRepository.Actualizar(respuesta);

            foreach (var item in dto.Campos)
            {
                var campo = campoRepository.ObtenerPorId(item.CAM_Campo);
                if (campo == null) continue;

                var detalle = new ACE_RESPUESTA_DETALLE
                {
                    RES_Respuesta = respuesta.RES_Respuesta,
                    CAM_Campo = item.CAM_Campo
                };

                switch (item.TCA_Clave)
                {
                    case "TXT":
                        detalle.RED_Valor_Texto = item.ValorTexto;
                        break;
                    case "NUM":
                        detalle.RED_Valor_Numero = item.ValorNumero;
                        break;
                    case "FEC":
                        if (!string.IsNullOrWhiteSpace(item.ValorFecha))
                            detalle.RED_Valor_Fecha = DateTime.Parse(item.ValorFecha, CultureInfo.InvariantCulture);
                        break;
                }

                detalle = detalleRepository.Crear(detalle);
                detalle.RED_Codigo = $"RED-{detalle.RED_Detalle:D6}";
                detalleRepository.Actualizar(detalle);

                if (item.TCA_Clave == "SELU" && item.OpcionSeleccionada.HasValue)
                {
                    var rop = new ACE_RESPUESTA_OPCION
                    {
                        RED_Detalle = detalle.RED_Detalle,
                        OPC_Opcion = item.OpcionSeleccionada.Value
                    };

                    rop = respuestaOpcionRepository.Crear(rop);
                    rop.ROP_Codigo = $"ROP-{rop.ROP_Respuesta_Opcion:D6}";
                    respuestaOpcionRepository.Actualizar(rop);
                }

                if (item.TCA_Clave == "SELM" && item.OpcionesSeleccionadas != null && item.OpcionesSeleccionadas.Any())
                {
                    foreach (var opcionId in item.OpcionesSeleccionadas)
                    {
                        var rop = new ACE_RESPUESTA_OPCION
                        {
                            RED_Detalle = detalle.RED_Detalle,
                            OPC_Opcion = opcionId
                        };

                        rop = respuestaOpcionRepository.Crear(rop);
                        rop.ROP_Codigo = $"ROP-{rop.ROP_Respuesta_Opcion:D6}";
                        respuestaOpcionRepository.Actualizar(rop);
                    }
                }
            }
        }  
    }
}