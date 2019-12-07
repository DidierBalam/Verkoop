using System;
using System.Collections.Generic;
using Verkoop.CapaDatos;
using Verkoop.CapaDatos.DTO;
using System.Linq;

namespace Verkoop.Business
{
    public class TarjetaBusiness
    {

        /// <summary>
        /// Método para guardar la primera tarjeta y marcarla por defecto, para ser el determinado para realizar compras.
        /// </summary>
        /// <param name="_objDatosTarjeta">Es un objeto que contiene los datos de la tarjeta</param>
        /// <returns>Retorna el estado de la operación y su mensaje de confirmación.</returns>
        public object GuardarPrimeraTarjeta(tblTarjeta _objDatosTarjeta)
        {
            string _cMensaje;///Se crea variable tipo string.
            bool _bEstadoTarjeta;///Se crea variable tipo booleano.

            try ///Se usa try catch para controlar los posibles errores que surgen.
            {
                using (VerkoopDBEntities _ctx = new VerkoopDBEntities()) ///Se crea contexto
                {
                    if (!VerificarExistenciaTarjeta(_ctx, _objDatosTarjeta))
                    {
                        _objDatosTarjeta.lDefault = true;///Permite guardar la primera tarjeta como default.

                        _ctx.tblTarjeta.Add(_objDatosTarjeta);///Se inserta las propiedades del objeto  _objDatosTarjeta a la tabla tblTarjeta.
                        _ctx.SaveChanges();///Se guardan cambios.

                        _cMensaje = "Tarjeta guardada";///
                        _bEstadoTarjeta = true;
                    }
                    else
                    {
                        _cMensaje = "La tarjeta ya se encuentra registrada.";
                        _bEstadoTarjeta = false;

                    }
                }
            }
            catch (Exception)
            {
                _cMensaje = "Ocurrió un error al momento de guardar la tarjeta.";
                _bEstadoTarjeta = false;
            }

            return (new { _bEstadoTarjeta, _cMensaje });
        }

        /// <summary>
        /// Método para guardar una tarjeta nueva.
        /// </summary>
        /// <param name="_objDatosTarjeta">Objeto que contiene los datos de la tarjeta.</param>
        /// <returns>Retorna el estado de la operación y su mensaje de confirmación.</returns>
        public object GuardarTarjeta(tblTarjeta _objDatosTarjeta)
        {
            string _cMensaje;///Se crea variable tipo string.
            bool _bEstadoTarjeta;///Se crea variable tipo booleano.

            try ///Se usa try catch para controlar los posibles errores que surgen.
            {
                using (VerkoopDBEntities _ctx = new VerkoopDBEntities()) ///Se crea contexto
                {
                    if (!VerificarExistenciaTarjeta(_ctx, _objDatosTarjeta))
                    {
                        _objDatosTarjeta.lDefault = false;  ///Permite guardar las tarjetas con valor default en falso.

                        _ctx.tblTarjeta.Add(_objDatosTarjeta);///Se inserta las propiedades del objeto  _objDatosTarjeta a la tabla tblTarjeta.
                        _ctx.SaveChanges();///Se guardan cambios.

                        _cMensaje = "¡Tarjeta guardada con éxito!";
                        _bEstadoTarjeta = true;

                    }
                    else
                    {
                        _cMensaje = "La tarjeta ya se encuentra registrada.";
                        _bEstadoTarjeta = false;

                    }
                }
            }
            catch (Exception)
            {
                _cMensaje = "Ocurrió un error al momento de guardar la tarjeta.";
                _bEstadoTarjeta = false;
            }

            return (new { _bEstadoTarjeta, _cMensaje, _objDatosTarjeta });
        }

        /// <summary>
        /// Método para eliminar una tarjeta.
        /// </summary>
        /// <param name="_iIdTarjeta"> Recibe el Id de la tarjeta a eliminar.</param>
        /// <returns>Retorna el estado de la operación y su mensaje de confirmación.</returns>
        public object EliminarTarjeta(int _iIdTarjeta)
        {
            string _cMensaje;///Se declara la variable string.
            bool _bEstadoOperación;///Se declara la variable bool.

            try ///Se usa try catch para controlar las excepciones.
            {
                using (VerkoopDBEntities _ctx = new VerkoopDBEntities()) ///Se crea contexto.
                {
                    tblTarjeta _TablaTarjeta = new tblTarjeta /// Se crea una instancia de la tabla tblTarjeta.
                    {
                        iIdTarjeta = (from Tarjeta in _ctx.tblTarjeta///Se usa linq para buscar el id de la tarjeta a eliminar.
                                      where Tarjeta.iIdTarjeta == _iIdTarjeta/// Cuando el id de la tarjeta de la base de datos sea igual al id que se quiere eliminar se selecciona.
                                      select Tarjeta.iIdTarjeta).First()///Se selecciona el campo que contiene el id a eliminar.
                    };

                    _ctx.tblTarjeta.Remove(_TablaTarjeta);///Se elimina el registro de la tabla.
                    _ctx.SaveChanges();///Se guardan los cambios.

                    _cMensaje = "La tarjeta ha sido eliminada";///Se envía el mensaje cuando se elimine el id de la tarjeta.
                    _bEstadoOperación = true;///Si se elimina el id el estado de la operación es verdadero.

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
        /// Método para conectar con ObtenerTodasTarjetas(), para obtener el total de tarjetas.
        /// </summary>
        /// <param name="_iIdUsuario">Contiene el id del usuario</param>
        /// <returns>Retorna la lista de la consulta</returns>
        public List<TarjetaDTO> ObtenerTodasTarjetas(int _iIdUsuario)
        {

            using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
            {

                List<TarjetaDTO> _lstTarjetas = (from Tarjeta in _ctx.tblTarjeta
                                                 where Tarjeta.iIdUsuario == _iIdUsuario
                                                 select new TarjetaDTO
                                                 {
                                                     iIdTarjeta = Tarjeta.iIdTarjeta,
                                                     cNumeroTarjeta = Tarjeta.cNumeroTarjeta,
                                                     cMesVigencia = Tarjeta.cMesVigencia,
                                                     cAnioVigencia = Tarjeta.cAnioVigencia
                                                 }).ToList();

                return _lstTarjetas;

            }

        }

        /// <summary>
        /// Método para verficar la existencia de la tarjeta en la base de datos.
        /// </summary>
        /// <param name="_objTargeta">Recibe los atributos de la tarjeta</param>
        /// <returns>Retorna un valor booleano como bandera</returns>
        public bool VerificarExistenciaTarjeta(VerkoopDBEntities _ctx, tblTarjeta _objTargeta)
        {
            bool _bCoincidencia = _ctx.tblTarjeta.Any(x => x.cNumeroTarjeta == _objTargeta.cNumeroTarjeta);

            return _bCoincidencia;
        }

    }
}
