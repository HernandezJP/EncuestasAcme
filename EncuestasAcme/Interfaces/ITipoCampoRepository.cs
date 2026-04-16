using EncuestasAcme.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncuestasAcme.Interfaces
{
    public interface ITipoCampoRepository
    {
        List<ACE_TIPO_CAMPO> ObtenerTodos();
        List<ACE_TIPO_CAMPO> ObtenerActivos();
        ACE_TIPO_CAMPO ObtenerPorId(int id);
        ACE_TIPO_CAMPO Crear(ACE_TIPO_CAMPO tipoCampo);
        void Actualizar(ACE_TIPO_CAMPO tipoCampo);
        bool ExisteClave(string clave, int? excluirId = null);
    }
}