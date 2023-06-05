using ApiAlejandro.Models;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAlejandro.Sql
{
    public class DTO_Usuario : GetConnectionDAO
    {
        public DTO_Usuario()
        {

        }

        public List<Models.Usuario> GetUsuarios()
        {
            var listaUsuarios = new List<Models.Usuario>();
            try
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"call getUsuarios()";
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var t = new Models.Usuario();
                    t.id_usuario = reader.GetFieldValue<int>(0);
                    t.nombre = reader.GetFieldValue<string>(1);
                    t.email = reader.GetFieldValue<string>(2);
                    t.contrasena = reader.GetFieldValue<string>(3);


                    listaUsuarios.Add(t);
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
            return listaUsuarios;
        }
        public Int64 VerificarEmail(Models.Request request)
        {
            Int64 existe=0;
            try
            {
                var cmd = connection.CreateCommand();
                var sql = @"call VerificarEmail('@email')";
                sql = sql.Replace("@email",request.email);
                Console.WriteLine(sql);

                cmd.CommandText = sql;
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var t = reader.GetFieldValue<Int64>(0);
                    existe = t;
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
            return existe;

        }
        public Usuario verificarLogin(Models.Request request)
        {
            var usuario = new Usuario();
            try
            {
                var cmd = connection.CreateCommand();
                var sql = @"call VerificarLogin('@email','@contrasena')";
                sql = sql.Replace("@email", request.email);
                sql = sql.Replace("@contrasena", request.contrasena);
                cmd.CommandText = sql;
                connection.Open();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    
                    usuario.id_usuario = reader.GetFieldValue<int>(0);
                    usuario.nombre = reader.GetFieldValue<string>(1);
                    usuario.email = reader.GetFieldValue<string>(2);
                    usuario.contrasena = reader.GetFieldValue<string>(3);

                }
            }
            catch (Exception ex)
            {
                // Aquí puedes registrar o manejar el error de alguna manera
            }
            finally
            {
                connection.Close();
            }
            return usuario;
        }


        public Models.Response putUsuario(Models.Request request)
        {
            var response = new Models.Response();
            try
            {
                var cmd = connection.CreateCommand();
                var sql = @"call putUsuario('@nombre','@email','@contrasena')";
                sql = sql.Replace("@nombre", request.nombre);
                sql = sql.Replace("@email", request.email);
                sql = sql.Replace("@contrasena", request.contrasena);

                cmd.CommandText = sql;
                connection.Open();
                cmd.ExecuteNonQuery();
                response.OK = "Usuario añadido correctamente";
            }
            catch (Exception ex)
            {
                response.Error = "Error interno en insertar " + ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return response;
        }

    }
}