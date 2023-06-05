using ApiAlejandro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAlejandro.Sql
{
    public class DTO_Producto : GetConnectionDAO
    {
        public DTO_Producto()
        {

        }

        public List<Models.Producto> GetProductos()
        {
            var listaProductos = new List<Models.Producto>();
            try
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"call getProductos()";
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var t = new Models.Producto();
                    t.id_producto = reader.GetFieldValue<int>(0);
                    t.nombre = reader.GetFieldValue<string>(1);
                    t.descripcion = reader.GetFieldValue<string>(2);
                    t.precio = reader.GetFieldValue<decimal>(3);
                    t.cantidad = reader.GetFieldValue<int>(4);
                    t.imagen = reader.GetFieldValue<string>(5);
                    

                    listaProductos.Add(t);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return listaProductos;
        }


        public Models.Response putProducto(Models.Request request)
        {
            var response = new Models.Response();
            try
            {
                var cmd=connection.CreateCommand();
                var sql = @"call putProducto('@nombre','@descripcion','@precio','@imagen')";
                sql = sql.Replace("@nombre", request.nombre);
                sql = sql.Replace("@descripcion", request.descripcion);
                sql = sql.Replace("@precio", request.precio.ToString());
                sql = sql.Replace("@imagen", request.imagen);

                cmd.CommandText = sql;
                connection.Open();
                cmd.ExecuteNonQuery();
                response.OK = "Producto añadido correctamente";
            }
            catch(Exception ex)
            {
                response.Error = "Error interno en insertar " + ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return response;
        }

        public Models.Response postProducto(Models.Request request)
        {
            var response=new Models.Response();
            try
            {
                var cmd=connection.CreateCommand();
                var sql = @"call postProducto('@nombre',@id,'@descripcion','@precio')";
                sql = sql.Replace("@nombre",request.nombre);
                 sql = sql.Replace("@id", request.Id.ToString());
                sql = sql.Replace("@descripcion", request.descripcion);
                sql = sql.Replace("@precio", request.precio.ToString());
                cmd.CommandText = sql;
                connection.Open();
                cmd.ExecuteNonQuery();
                response.OK = "Producto actualizado correctamente";

                var producto=new Models.Producto();
                producto.id_producto=request.Id;
                producto.nombre = request.nombre;
                response.Data = producto;
            }
            catch (Exception ex)
            {
                response.Error = "Error interno en actualizar: "+ex.Message;

            }
            finally
            {
                connection.Close();
            }
            return response;
        }

        public Models.Response deleteProducto(Models.Request request)
        {
            var response = new Models.Response();
            try
            {
                var cmd= connection.CreateCommand();
                var sql = @"call deleteProducto(@id)";
                sql = sql.Replace("@id",request.Id.ToString());
                connection.Open();
                cmd.ExecuteNonQuery();
                response.OK = "Producto eliminado correctamente";
            }
            catch (Exception ex)
            {
                response.Error="Error intentando borrar en : "+ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return response;
        }
    }
}