﻿using System;
using System.Collections.Generic;
using System.Linq;
using Verkoop.CapaDatos;
using Verkoop.CapaDatos.DTO;

namespace Verkoop.Business
{
    public class DireccionBusiness
    {
        /// <summary>
        /// Método para actualizar una dirección.
        /// </summary>
        /// <param name="_objDatosDireccion"> Es un objeto que contiene los datos de la dirección</param>
        /// <returns>Retorna el estado de la operación y su mensaje de confirmación.</returns>
        public object ActualizarDireccion(tblDireccion _objDatosDireccion)
        {
            string _cMensaje; ///Se crea variable tipo string.
            bool _bEstadoOperacion;///Se crea variable tipo booleano.

            try ///Se usa try catch para controlar los posibles errores que surgen.
            {
                using (VerkoopDBEntities _ctx = new VerkoopDBEntities()) /// Se crea contexto
                {
                    tblDireccion _TablaDireccion = new tblDireccion(); /// Instancia de tblDirección.

                    _TablaDireccion = (from Direccion in _ctx.tblDireccion
                                       where Direccion.iIdDireccion == _objDatosDireccion.iIdDireccion
                                       select Direccion).First();

                    _TablaDireccion.iIdDireccion = _objDatosDireccion.iIdDireccion;
                    _TablaDireccion.cCodigoPostal = _objDatosDireccion.cCodigoPostal;
                    _TablaDireccion.cDireccion = _objDatosDireccion.cDireccion;
                    _TablaDireccion.iIdMunicipio = _objDatosDireccion.iIdMunicipio;

                    _ctx.SaveChanges();

                    _cMensaje = "Se han actualizado los datos con éxito";
                    _bEstadoOperacion = true;

                }
            }
            catch (Exception)
            {
                _cMensaje = "Ocurrió un error al momento de actualizar, intente de nuevo.";
                _bEstadoOperacion = false;

            }

            return (new { _bEstadoOperacion, _cMensaje });

        }


        /// <summary>
        /// Método que se conecta a EliminarDireccion().
        /// </summary>
        /// <param name="_iIdDireccion">Recibe el id de la dirección.</param>
        /// <returns>Retorna el estado de la operación y su mensaje de confirmación.</returns>
        public object EliminarDireccion(int _iIdDireccion)
        {
            string _cMensaje;
            bool _bEstadoOperacion;

            try
            {
                using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
                {
                    tblDireccion _tabladireccion = (from Direccion in _ctx.tblDireccion
                                                    where Direccion.iIdDireccion == _iIdDireccion
                                                    select Direccion).SingleOrDefault();

                    _ctx.tblDireccion.Remove(_tabladireccion);
                    _ctx.SaveChanges();

                    _cMensaje = "La dirección ha sido eliminada";
                    _bEstadoOperacion = true;
                }
            }
            catch (Exception)
            {

                _cMensaje = "¡Algo salio mal, la tarjeta no se pudo eliminar!";
                _bEstadoOperacion = false;
            }
            return (new { _bEstadoOperacion, _cMensaje });
        }


        /// <summary>
        /// Método para conectarse con GuardarDirección()
        /// </summary>
        /// <param name="_objDatosDireccion">Recibe los datos de dirección en forma de objetos.</param>
        /// <returns>Retorna el estado de la dirección, su mensaje de confirmación y la dirección.</returns>
        public object GuardarDireccion(tblDireccion _objDatosDireccion)
        {
            string _cMensaje;
            bool _bEstadoDireccion;

            try
            {
                using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
                {
                    _objDatosDireccion.lDefault = false;
                    _ctx.tblDireccion.Add(_objDatosDireccion);
                    _ctx.SaveChanges();

                    _cMensaje = "La dirección se ha aguardado.";
                    _bEstadoDireccion = true;

                }

            }
            catch (Exception)
            {
                _cMensaje = "Ocurrió un error al momento de guardar la dirección intente de nuevo.";
                _bEstadoDireccion = false;

            }

            return (new { _bEstadoDireccion, _cMensaje, _objDatosDireccion });
        }


        /// <summary>
        /// Método para conectar con ObtenerDireccionesDeUsuario().
        /// </summary>
        /// <param name="_iIdUsuario">Contiene el id del usuario.</param>
        /// <returns>Retorna la lista de la consulta</returns>
        public List<DireccionDTO> ObtenerDireccionesDeUsuario(int _iIdUsuario)
        {

            using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
            {
                List<DireccionDTO> _lstDirecciones = (from Direccion in _ctx.tblDireccion
                                                      where Direccion.iIdUsuario == _iIdUsuario
                                                      join Municipio in _ctx.tblMunicipio on Direccion.iIdMunicipio equals Municipio.iIdMunicipio
                                                      join Estado in _ctx.tblEstado on Municipio.iIdEstado equals Estado.iIdEstado
                                                      join Pais in _ctx.tblPais on Estado.iIdPais equals Pais.iIdPais

                                                      select new DireccionDTO
                                                      {
                                                          iIdDireccion = Direccion.iIdDireccion,
                                                          cMunicipio = Municipio.cNombre,
                                                          cEstado = Estado.cNombre,
                                                          cPais = Pais.cNombre,
                                                          cDireccion = Direccion.cDireccion,
                                                          cCodigoPostal = Direccion.cCodigoPostal,
                                                          bEstado = Direccion.lDefault

                                                      }).ToList();

                return _lstDirecciones;
            }


        }

        /// <summary>
        /// MÉTODO PARA OBTENER TODOS LOS PAISES.
        /// </summary>
        /// <returns>Retorna una lista con todos los paises.</returns>
        public List<tblPais> ObtenerTodosPaises()
        {
            using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
            {
                _ctx.Configuration.LazyLoadingEnabled = false;

                List<tblPais> _lstPais = _ctx.tblPais.AsNoTracking().ToList();

                return _lstPais;
            }


        }

        /// <summary>
        /// MÉTODO PARA OBTENER LOS ESTADOS DE UN PAÍS.
        /// </summary>
        /// <param name="_iIdPais">Recibe el Id del país</param>
        /// <returns>Retorna una lista con los estados</returns>
        public List<tblEstado> ObtenerEstadosPorPais(int _iIdPais)
        {
            using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
            {
                _ctx.Configuration.LazyLoadingEnabled = false;

                List<tblEstado> _lstEstado = _ctx.tblEstado.Where(x => x.iIdPais == _iIdPais).ToList();

                return _lstEstado;
            }
        }

        /// <summary>
        /// MÉTODO PARA OBTENER LOS MUNICIPIOS DE UN ESTADO.
        /// </summary>
        /// <param name="_iIdPais">Recibe el Id del estado</param>
        /// <returns>Retorna una lista con los Municipios</returns>
        public List<tblMunicipio> ObtenerMunicipiosPorEstado(int _iIEstado)
        {
            using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
            {
                _ctx.Configuration.LazyLoadingEnabled = false;

                List<tblMunicipio> _lstMunicipio = _ctx.tblMunicipio.Where(x => x.iIdEstado == _iIEstado).ToList();

                return _lstMunicipio;

            }

        }

        /// <summary>
        /// MÉTODO PARA OBTENER LA DIRECCIÓN PREDETERMINADA
        /// </summary>
        /// <param name="_idUsuario">Recibe el id del usuario</param>
        /// <returns>Retorna el objeto dirección</returns>
        public tblDireccion ObtenerDireccionPredeterminada(int _idUsuario)
        {
            using(VerkoopDBEntities _ctx = new VerkoopDBEntities())
            {

                tblDireccion _objDireccion = _ctx.tblDireccion.AsNoTracking().Where(x => x.iIdUsuario == _idUsuario && x.lDefault == true).FirstOrDefault();

                return _objDireccion;
            
            }

        }
    }
}
