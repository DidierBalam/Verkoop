﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verkoop.CapaDatos.DTO;

namespace Verkoop.Business
{
    class DireccionBusiness
    {

        public bool ActualizarDireccion(DireccionDTO _objDatosDireccion)
        {

            return true;
        }

        public bool EliminarDirecion(int _iIdDireccion)
        {

            return true;
        }

       
        public List<DireccionDTO> GuardarDireccion(DireccionDTO _objDatosDireccion)
        {

            return null;
        }

      
        public List<DireccionDTO> ObtenerDireccionesDeUsuario(int _iIdUsuario)
        {

            return null;
        }
    }
}