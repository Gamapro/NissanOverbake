﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;
using MySql.Data.MySqlClient;

namespace DataLayer
{
    public class PlcLogsApi
    {
        private static string FormatToDate(string dateString)
        {
            DateTime date = Convert.ToDateTime(dateString);
            return date.ToString("yyyy-MM-dd");
        }
        //// ############################## PLC LOGS ##################################

        public static List<PlcLog> ListLogs(string fechaInicio = "", string fechaFin = "")
        {
            List<PlcLog> list = new List<PlcLog>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Conection.cn))
                {
                    string query = "";
                    if (String.IsNullOrEmpty(fechaInicio) || String.IsNullOrEmpty(fechaFin))
                    {
                        query = "select pl.id, pl.time, p.name, pl.message from plclogs pl join plcs p on p.id = pl.plcId order by pl.time limit 10000;";
                    }
                    else
                    {
                        fechaInicio = FormatToDate(fechaInicio);
                        fechaFin = FormatToDate(fechaFin);
                        query = String.Format("select pl.id, pl.time, p.name, pl.message from plclogs pl join plcs p on p.id = pl.plcId where pl.time between'{0}' and '{1}' order by pl.time limit 10000;", fechaInicio, fechaFin);
                    }
                    Debug.WriteLine("QUERY ListLogs ", query);
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        connection.Open();
                        using (MySqlDataReader sqldr = cmd.ExecuteReader())
                        {
                            while (sqldr.Read())
                            {
                                list.Add(new PlcLog()
                                {
                                    Id = Convert.ToInt32(sqldr["id"]),
                                    Time = Convert.ToString(sqldr["time"]),
                                    Message = sqldr["message"].ToString(),
                                    PlcName = sqldr["name"].ToString(),
                                });
                            }
                        }
                    }
                }
            }
            catch (MySqlException e) { Debug.WriteLine("MySqlException listLogs", e.Message); throw e; }
            catch (Exception e) { Debug.WriteLine(e.Message); throw e; }
            return list;
        }
        public static bool InsertPlcLog(PlcLog log, out string message)
        {
            bool excecuted = false;
            message = "";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Conection.cn))
                {
                    string query = String.Format("insert into plclogs (time, plcId, message) values ('{0}', {1}, '{2}');", log.Time, log.PlcId, log.Message);
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
        public static bool UpdatePlcLog(PlcLog log, out string message)
        {
            bool excecuted = false;
            message = "";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Conection.cn))
                {
                    string query = String.Format("update plclogs set time='{0}', plcId={1}, message='{2}' where id={3};", log.Time, log.PlcId, log.Message, log.Id);
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
        public static bool DeletePlcLog(PlcLog log, out string message)
        {
            bool excecuted = false;
            message = "";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Conection.cn))
                {
                    string query = String.Format("delete from plclogs where id={0};", log.Id);
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
