using EncuestasAcme.DTOs.TipoCampo;
using EncuestasAcme.Interfaces;
using EncuestasAcme.Models;
using EncuestasAcme.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncuestasAcme.Services
{
    public class TipoCampoService 
    {
        private readonly ITipoCampoRepository repository;

        public TipoCampoService()
        {
            repository = new TipoCampoRepository();
        }

        public List<ResponseTipoCampoDTO> ObtenerTodos()
        {
            return repository.ObtenerTodos()
                .Select(x => new ResponseTipoCampoDTO
                {
                    TCA_Tipo_Campo = x.TCA_Tipo_Campo,
                    TCA_Codigo = x.TCA_Codigo,
                    TCA_Clave = x.TCA_Clave,
                    TCA_Descripcion = x.TCA_Descripcion,
                    TCA_Permite_Opciones = x.TCA_Permite_Opciones,
                    TCA_Permite_Multiples = x.TCA_Permite_Multiples,
                    TCA_Estado = x.TCA_Estado,
                    TCA_Fecha_Creacion = x.TCA_Fecha_Creacion
                })
                .ToList();
        }

        public List<ResponseTipoCampoDTO> ObtenerActivos()
        {
            return repository.ObtenerActivos()
                .Select(x => new ResponseTipoCampoDTO
                {
                    TCA_Tipo_Campo = x.TCA_Tipo_Campo,
                    TCA_Codigo = x.TCA_Codigo,
                    TCA_Clave = x.TCA_Clave,
                    TCA_Descripcion = x.TCA_Descripcion,
                    TCA_Permite_Opciones = x.TCA_Permite_Opciones,
                    TCA_Permite_Multiples = x.TCA_Permite_Multiples,
                    TCA_Estado = x.TCA_Estado,
                    TCA_Fecha_Creacion = x.TCA_Fecha_Creacion
                })
                .ToList();
        }

        public ACE_TIPO_CAMPO ObtenerPorId(int id)
        {
            return repository.ObtenerPorId(id);
        }

        public void Crear(CreateTipoCampoDTO dto)
        {
            if (repository.ExisteClave(dto.TCA_Clave))
            {
                throw new Exception("Ya existe un tipo de campo con esa clave.");
            }

            if (!dto.TCA_Permite_Opciones)
            {
                dto.TCA_Permite_Multiples = false;
            }

            var tipoCampo = new ACE_TIPO_CAMPO
            {
                TCA_Clave = dto.TCA_Clave.Trim().ToUpper(),
                TCA_Descripcion = dto.TCA_Descripcion.Trim(),
                TCA_Permite_Opciones = dto.TCA_Permite_Opciones,
                TCA_Permite_Multiples = dto.TCA_Permite_Multiples,
                TCA_Estado = "A",
                TCA_Fecha_Creacion = DateTime.Now
            };

            tipoCampo = repository.Crear(tipoCampo);
            tipoCampo.TCA_Codigo = $"TCA-{tipoCampo.TCA_Tipo_Campo:D6}";
            repository.Actualizar(tipoCampo);
        }

        public void Actualizar(UpdateTipoCampoDTO dto)
        {
            var tipoCampo = repository.ObtenerPorId(dto.TCA_Tipo_Campo);

            if (tipoCampo == null)
            {
                throw new Exception("El tipo de campo no existe.");
            }

            if (repository.ExisteClave(dto.TCA_Clave.Trim().ToUpper(), dto.TCA_Tipo_Campo))
            {
                throw new Exception("Ya existe otro tipo de campo con esa clave.");
            }

            if (!dto.TCA_Permite_Opciones)
            {
                dto.TCA_Permite_Multiples = false;
            }

            tipoCampo.TCA_Clave = dto.TCA_Clave.Trim().ToUpper();
            tipoCampo.TCA_Descripcion = dto.TCA_Descripcion.Trim();
            tipoCampo.TCA_Permite_Opciones = dto.TCA_Permite_Opciones;
            tipoCampo.TCA_Permite_Multiples = dto.TCA_Permite_Multiples;

            repository.Actualizar(tipoCampo);
        }

        public void EliminarLogico(int id)
        {
            var tipoCampo = repository.ObtenerPorId(id);

            if (tipoCampo == null)
            {
                throw new Exception("El tipo de campo no existe.");
            }

            tipoCampo.TCA_Estado = "I";
            repository.Actualizar(tipoCampo);
        }

        public void Reactivar(int id)
        {
            var tipoCampo = repository.ObtenerPorId(id);

            if (tipoCampo == null)
            {
                throw new Exception("El tipo de campo no existe.");
            }

            tipoCampo.TCA_Estado = "A";
            repository.Actualizar(tipoCampo);
        }
    }
}