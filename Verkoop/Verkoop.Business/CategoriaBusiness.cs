using System;
using System.Collections.Generic;
using System.Linq;
using Verkoop.CapaDatos.DTO;
using Verkoop.CapaDatos;

namespace Verkoop.Business
{
    public class CategoriaBusiness
    {
        /// <summary>
        /// Método para agregar una categoría.
        /// </summary>
        /// <param name="_cNombreCategoria">String que contiene el nombre de la categoría.</param>
        /// <returns>Retorna el estado de la operación y su mensaje de confirmación.</returns>
        public object GuardarCategoria(string _cNombreCategoria)
        {
            string _cMensaje;
            bool _bEstadoCategoria;

            try
            {
                using (VerkoopDBEntities _ctx = new VerkoopDBEntities())
                {
                    
                    tblCat_Categoria _TablaCategoria = new tblCat_Categoria();

                    _TablaCategoria.cNombre = _cNombreCategoria;
                    _ctx.tblCat_Categoria.Add(_TablaCategoria);
                    _ctx.SaveChanges();

                    _cMensaje = "Categoría agregada con éxito!";
                    _bEstadoCategoria = true;
                }
            }
            catch (Exception e)
            {
                _cMensaje = e.Message;
                _bEstadoCategoria = false;

            }
            return (new { _cMensaje, _bEstadoCategoria });
        }

        /// <summary>
        /// Método para conectar con EliminarCategoria().
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
                    tblCat_Categoria _TablaCategoria = new tblCat_Categoria();

                    _TablaCategoria.iIdCategoria = (from Categoria in _ctx.tblCat_Categoria
                                                    where Categoria.iIdCategoria == _iIdCategoria
                                                    select Categoria.iIdCategoria).First();

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
        /// Método para conectar con ObtenerCategorias().
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
