using ApiAlejandro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAlejandro.Sql
{
    public class DTO_Comentario : GetConnectionDAO
    {
        public DTO_Comentario()
        {

        }

        public List<Models.Comentario> GetComentarios()
        {
            var listaComentarios = new List<Models.Comentario>();
            try
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"call getComentarios()";
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var t = new Models.Comentario();
                    t.id_comentario = reader.GetFieldValue<int>(0);
                    t.id_producto = reader.GetFieldValue<int>(1);
                    t.contenido = reader.GetFieldValue<string>(2);
                    t.fecha = reader.GetFieldValue<DateTime>(3);


                    listaComentarios.Add(t);
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
            return listaComentarios;
        }


        public Models.Response putComentario(Models.Request request)
        {
            var response = new Models.Response();
            try
            {
                var cmd=connection.CreateCommand();
                var sql = @"call putComentario('@id_producto','@contenido','@id_usuario')";
                sql = sql.Replace("@id_producto", request.id_producto.ToString());
                sql = sql.Replace("@contenido", request.contenido);
                sql = sql.Replace("@id_usuario", request.id_usuario.ToString());

                cmd.CommandText = sql;
                connection.Open();
                cmd.ExecuteNonQuery();
                response.OK = "Comentario añadido correctamente";
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

        public Models.Response postComentario(Models.Request request)
        {
            var response=new Models.Response();
            try
            {
                var cmd=connection.CreateCommand();
                var sql = @"call postComentario('@contenido',@id)";
                sql = sql.Replace("@contenido", request.descripcion);
                 sql = sql.Replace("@id", request.Id.ToString());
                cmd.CommandText = sql;
                connection.Open();
                cmd.ExecuteNonQuery();
                response.OK = "Comentario actualizado correctamente";

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

        public Models.Response deleteComentario(Models.Request request)
        {
            var response = new Models.Response();
            try
            {
                var cmd= connection.CreateCommand();
                var sql = @"call deleteComentario(@id)";
                sql = sql.Replace("@id",request.Id.ToString());
                connection.Open();
                cmd.ExecuteNonQuery();
                response.OK = "Comentario eliminado correctamente";
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