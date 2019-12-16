using System;
using System.Collections.Generic;
using System.Linq;
using Verkoop.CapaDatos.DTO;
using Verkoop.CapaDatos;
using System.Data.Entity;

namespace Verkoop.Business
{
    public class CategoriaBusiness
    {
        private string _cMensaje = string.Empty;
        private string _EstadoConsulta = string.Empty;
       

        /// <summary>
        /// MÉTODO PARA AGREGAR UNA CATEGORÍA.
        /// </summary>
        /// <param name="_cNombreCategoria">String que contiene el nombre de la categoría.</param>
        /// <returns>Retorna el estado de la operación y su mensaje de confirmación.</returns>
        public object GuardarCategoria(string _cNombreCategoria)
        {
            tblCat_Categoria _TablaCategoria = new tblCat_Categoria();
            try
            {
                using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
                {

                    _TablaCategoria.cNombre = _cNombreCategoria;

                    _ctx.Entry(_TablaCategoria).State = EntityState.Added;
                    _ctx.SaveChanges();

                    _cMensaje = "Categoría agregada con éxito!";
                    _EstadoConsulta = "success";
                    //_bEstadoCategoria = true;
                }
            }
            catch (Exception )
            {
                _cMensaje = "Algo falló, no se pudo agregar categoría, intente de nuevo.";
                _EstadoConsulta = "error";
                //_bEstadoCategoria = false;

            }
            return (new { EstadoConsulta = _EstadoConsulta, Mensaje = _cMensaje, tarjeta = _TablaCategoria });
        }

        /// <summary>
        /// MÉTODO PARA CONECTAR CON ELIMINARCATEGORIA().
        /// </summary>
        /// <param name="_iIdCategoria">Contiene el id de la categoría.</param>
        /// <returns>Retorna el estado de la operación y su mensaje de confirmación.</returns>
        public object EliminarCategoria(int _iIdCategoria)
        {
            string _cMensaje;
            bool _bEstadoCategoria;

            try
            {
                using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
                {
                    tblCat_Categoria _TablaCategoria = new tblCat_Categoria
                    {
                        iIdCategoria = (from Categoria in _ctx.tblCat_Categoria
                                        where Categoria.iIdCategoria == _iIdCategoria
                                        select Categoria.iIdCategoria).First()
                    };

                    _ctx.tblCat_Categoria.Remove(_TablaCategoria);
                    _ctx.SaveChanges();

                    _cMensaje = "¡Categoría eliminada con éxito!.";
                    _bEstadoCategoria = true;


                }

            }
            catch (Exception)
            {
                _cMensaje = "¡Wow, la categoría no se pudo eliminar!";
                _bEstadoCategoria = false;
            }

            return (new { _bEstadoCategoria, _cMensaje });
        }

        /// <summary>
        /// MÉTODO PARA CONECTAR CON OBTENERCATEGORIAS().
        /// </summary>
        /// <param name="_iIdUsuario">Contiene el id del usuario.</param>
        /// <returns>Retorna la lista de la consulta.</returns>
        public List<CategoriaDTO> ObtenerCategorias()
        {
            using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
            {
                List<CategoriaDTO> _lstCategorias = (from Categoria in _ctx.tblCat_Categoria
                                                     select new CategoriaDTO
                                                     {
                                                         iIdCategoria = Categoria.iIdCategoria,
                                                         cNombre = Categoria.cNombre

                                                     }).ToList();
                return _lstCategorias;

            }
        }
    }
}
