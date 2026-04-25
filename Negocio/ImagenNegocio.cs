using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ImagenNegocio
    {
        // Listar imágenes por Artículo
        public List<Imagen> listarImagenesPorArticulo(int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Imagen> lista = new List<Imagen>();
            try
            {
                datos.setearConsulta("Select Id, IdArticulo, ImagenUrl from IMAGENES where IdArticulo = @id");
                datos.setearParametro("@id", idArticulo);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Imagen aux = new Imagen();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.IdArticulo = (int)datos.Lector["IdArticulo"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // Obtener la primer imagen de un artículo
        public string obtenerImagen(int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"SELECT TOP 1 ImagenUrl FROM IMAGENES WHERE IdArticulo = @id ORDER BY Id ASC");
                datos.setearParametro("@id", idArticulo);
                datos.ejecutarLectura();

                if (datos.Lector.Read() && !(datos.Lector["ImagenUrl"] is DBNull))
                    return (string)datos.Lector["ImagenUrl"];

                return "";
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void agregarImagen(Imagen nueva)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Insert into IMAGENES (IdArticulo, ImagenUrl) values (@idArticulo, @imagenUrl)");
                datos.setearParametro("@idArticulo", nueva.IdArticulo);
                datos.setearParametro("@imagenUrl", nueva.ImagenUrl);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificarImagen(Imagen imagen)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Update IMAGENES set ImagenUrl = @imagenUrl where Id = @id");
                datos.setearParametro("@id", imagen.Id);
                datos.setearParametro("@imagenUrl", imagen.ImagenUrl);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void eliminarImagen(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Delete from IMAGENES where Id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }
    }
}
