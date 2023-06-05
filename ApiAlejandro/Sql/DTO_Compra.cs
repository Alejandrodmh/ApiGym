using ApiAlejandro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAlejandro.Sql
{
    public class DTO_Compra : GetConnectionDAO
    {
        public DTO_Compra()
        {

        }

        public List<Models.Compra> GetCompras()
        {
            var listaCompras= new List<Models.Compra>();
            try
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"call getCompras()";
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var t = new Models.Compra();
                    t.id_compra = reader.GetFieldValue<int>(0);
                    t.nombre = reader.GetFieldValue<string>(1);
                    t.fecha = reader.GetFieldValue<DateTime>(2);
                    t.precio = reader.GetFieldValue<decimal>(3);


                    listaCompras.Add(t);
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
            return listaCompras;
        }


        public Models.Response putCompra(Models.Request request)
        {
            var response = new Models.Response();
            try
            {
                var cmd=connection.CreateCommand();
                var sql = @"call putCompra('@id_producto','@cantidad','@id_compra','@id_usuario')";
                sql = sql.Replace("@id_producto", request.id_producto.ToString());
                sql = sql.Replace("@cantidad", request.cantidad.ToString());
                sql = sql.Replace("@id_compra", request.id_compra.ToString());
                sql = sql.Replace("@id_usuario", request.id_usuario.ToString());

                cmd.CommandText = sql;
                connection.Open();
                cmd.ExecuteNonQuery();
                response.OK = "Compra añadida correctamente";
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


        public Models.Response deleteCompra(Models.Request request)
        {
            var response = new Models.Response();
            try
            {
                var cmd= connection.CreateCommand();
                var sql = @"call deleteCompra(@id)";
                sql = sql.Replace("@id",request.Id.ToString());
                connection.Open();
                cmd.ExecuteNonQuery();
                response.OK = "Compra eliminado correctamente";
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