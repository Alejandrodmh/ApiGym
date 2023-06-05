using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace ApiAlejandro.Sql
{
    public class DAO
    {
        private static string conString = "Server=localhost;Database=@bbdd;Uid=@user;Pwd=@password;";
        private static MySqlConnection connection=null;
        private static DAO accessMysql;
        private static String Bbdd { get; set; } = null;
        private static String user { get; set; } = null;
        private static String password { get; set; } = null;

        public static DAO instance(String Bbdd,String user,String password)
        {
            try
            {
                if(accessMysql != null)
                {
                    if(DAO.Bbdd != Bbdd || DAO.user !=user || DAO.password !=password)
                    {
                        connection.Close();
                        createInstance(Bbdd, user, password);
                    }
                }
                else
                {
                    createInstance(Bbdd, user, password);
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error al instanciar connection " + ex.Message);
            }
            return accessMysql;
        }
        private static void createInstance(String bbdd,String user,String passsword)
        {
            accessMysql = new DAO(bbdd,user,passsword);
            DAO.user = user;
            DAO.password = passsword;
            DAO.Bbdd = Bbdd;
        }
        private DAO(string bbdd, string user, string password)
        {
            try
            {
                var url = DAO.conString.Replace("@bbdd", bbdd);
                url = url.Replace("@user", user);
                url = url.Replace("@password", password);

                if (connection == null)
                {
                    connection = new MySqlConnection();
                    connection.ConnectionString = url;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear connection " + ex.Message);
            }
        }
        public MySqlConnection getConnection()
        {            
                return connection;
        }

        public Boolean check()
        {
            if (connection != null)
                return true;
                else
                    return false;
        }
    }
}