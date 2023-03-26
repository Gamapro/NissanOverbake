using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using EntityLayer;
using MySql.Data.MySqlClient;
using Renci.SshNet.Messages;

namespace DataLayer
{
    public class PlcApi
    {
        //// ################################ PLCS ####################################
        public static List<Plc> ListPlcs()
        {
            List<Plc> list = new List<Plc>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Conection.cn))
                {
                    string query = "select * from plcs;";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        connection.Open();
                        using (MySqlDataReader sqldr = cmd.ExecuteReader())
                        {
                            while (sqldr.Read())
                            {
                                list.Add(new Plc()
                                {
                                    Id = Convert.ToInt32(sqldr["id"]),
                                    Enabled = Convert.ToInt32(sqldr["enabled"]),
                                    Name = sqldr["name"].ToString(),
                                    LastConection = Convert.ToString(sqldr["lastConection"])
                                });
                            }
                        }
                    }
                }
            }
            catch (MySqlException e) { Debug.WriteLine("MySqlException ListPlcs", e.Message); throw e; }
            catch (Exception e) { Debug.WriteLine("Exception ListPlcs", e.Message); throw e; }
            return list;
        }
        public static string GetPlcLastConection(int id)
        {
            string date = "";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Conection.cn))
                {
                    string query = String.Format("select * from plcs where id={0};", id);
                    Debug.WriteLine("QUERY GetLastPlcConection ", query);
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        connection.Open();
                        using (MySqlDataReader sqldr = cmd.ExecuteReader())
                        {
                            while (sqldr.Read())
                            {
                                date = Convert.ToString(sqldr["lastConection"]);
                            }
                        }
                    }
                }
            }
            catch (MySqlException e) { Debug.WriteLine("MySqlException UpdatePlcLog", e.Message); throw e; }
            catch (Exception e) { Debug.WriteLine("Exception UpdatePlcLog", e.Message); throw e; }
            return date;
        }
            public static bool UpdatePlc(Plc log, out string message)
        {
            bool excecuted = false;
            message = "";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Conection.cn))
                {
                    string query = String.Format("update plcs set enabled={0}, lastConection={1} where id={2};", log.Enabled, log.LastConection, log.Id);
                    Debug.WriteLine("QUERY UpdatePlcLog ", query);
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            excecuted = true;
                            message = "Log actualizado";
                        }
                        else
                        {
                            excecuted = false;
                            message = "No se pudo actualizar el log";
                        }
                    }
                }
            }
            catch (MySqlException e) { Debug.WriteLine("MySqlException UpdatePlcLog", e.Message); throw e; }
            catch (Exception e) { Debug.WriteLine(e.Message); throw e; }
            return excecuted;
        }
        public static bool InsertPlc(Plc log, out string message)
        {
            bool excecuted = false;
            message = "";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Conection.cn))
                {
                    string query = String.Format("insert into plcs (name, enabled, lastConection) values ('{0}', {1}, {2});", log.Name, log.Enabled, log.LastConection);
                    Debug.WriteLine("QUERY InsertPlcLog ", query);
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            excecuted = true;
                            message = "Log insertado";
                        }
                        else
                        {
                            excecuted = false;
                            message = "No se pudo insertar el log";
                        }
                    }
                }
            }
            catch (MySqlException e) { Debug.WriteLine("MySqlException InsertPlcLog", e.Message); throw e; }
            catch (Exception e) { Debug.WriteLine(e.Message); throw e; }
            return excecuted;
        }
        public static bool DeletePlc(Plc log, out string message)
        {
            bool excecuted = false;
            message = "";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Conection.cn))
                {
                    string query = String.Format("delete from plcs where id={0};", log.Id);
                    Debug.WriteLine("QUERY DeletePlcLog ", query);
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            excecuted = true;
                            message = "Log eliminado";
                        }
                        else
                        {
                            excecuted = false;
                            message = "No se pudo eliminar el log";
                        }
                    }
                }
            }
            catch (MySqlException e) { Debug.WriteLine("MySqlException DeletePlcLog", e.Message); throw e; }
            catch (Exception e) { Debug.WriteLine(e.Message); throw e; }
            return excecuted;
        }
    }
}
