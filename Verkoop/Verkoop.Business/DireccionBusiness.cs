using System;
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
            bool _bEstadoOperación;///Se crea variable tipo booleano.

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
                    _bEstadoOperación = true;

                }
            }
            catch (Exception)
            {
                _cMensaje = "Ocurrió un error al momento de actualizar, intente de nuevo.";
                _bEstadoOperación = false;

            }

            return (new { _bEstadoOperación, _cMensaje });

        }


        /// <summary>
        /// Método que se conecta a EliminarDireccion().
        /// </summary>
        /// <param name="_iIdDireccion">Recibe el id de la dirección.</param>
        /// <returns>Retorna el estado de la operación y su mensaje de confirmación.</returns>
        public object EliminarDireccion(int _iIdDireccion)
        {
            string _cMensaje;
            bool _bEstadoOperación;

            try
            {
                using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
                {
                    tblDireccion _tabladireccion = (from Direccion in _ctx.tblDireccion
                                                    where Direccion.iIdDireccion == _iIdDireccion
                                                    select Direccion).SingleOrDefault();

                    _ctx.tblDireccion.Remove(_tabladireccion);
                    _ctx.SaveChanges();

                    _cMensaje = "La dirección ha sido eliminado";
                    _bEstadoOperación = true;
                }
            }
            catch (Exception)
            {

                _cMensaje = "¡Wow, la tarjeta no se pudo eliminar!";
                _bEstadoOperación = false;
            }
            return (new { _bEstadoOperación, _cMensaje });
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
    }
}
