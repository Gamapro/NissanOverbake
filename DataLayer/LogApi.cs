using System;
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
    public class LogApi
    {
        private static string FormatToDate(string dateString)
        {
            DateTime date = Convert.ToDateTime(dateString);
            return date.ToString("yyyy-MM-dd");
        }
        public static List<OverbakeLog> ListLogs(string fechaInicio = "", string fechaFin = "")
        {
            List<OverbakeLog> list = new List<OverbakeLog>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Conection.cn))
                {
                    string query = "";
                    if (String.IsNullOrEmpty(fechaInicio) || String.IsNullOrEmpty(fechaFin))
                    {
                        query = "select l.id, l.time, p.name, l.serialNumber, l.status, l.message from logs l join plcs p on p.id = l.plcId order by l.id desc limit 30;";
                    }
                    else
                    {
                        fechaInicio = FormatToDate(fechaInicio);
                        fechaFin = FormatToDate(fechaFin);
                        query = String.Format("select l.id, l.time, p.name, l.serialNumber, l.status, l.message from logs l join plcs p on p.id = l.plcId where l.time between'{0}' and '{1}' order by l.id desc limit 30;", fechaInicio, fechaFin);
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
                                list.Add(new OverbakeLog()
                                {
                                    Id = Convert.ToInt32(sqldr["id"]),
                                    Time = Convert.ToString(sqldr["time"]),
                                    PlcName = sqldr["name"].ToString(),
                                    SerialNumber = sqldr["serialNumber"].ToString(),
                                    Status = sqldr["status"].ToString(),
                                    Message = sqldr["message"].ToString(),
                                });
                            }
                        }
                    }
                }
            }
            catch (MySqlException e) { Debug.WriteLine("MySqlException ListLogs", e.Message); throw e; }
            catch (Exception e) { Debug.WriteLine("Exception ListLogs", e.Message); throw e; }
            return list;
        }
        public static List<LogCount> ListLogCount(string fechaInicio, string fechaFin)
        {
            List<LogCount> list = new List<LogCount>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Conection.cn))
                {   
                    string query = "";
                    if (String.IsNullOrEmpty(fechaInicio) || String.IsNullOrEmpty(fechaFin))
                    {
                        return list;
                    }
                    else
                    {
                        fechaInicio = FormatToDate(fechaInicio);
                        fechaFin = FormatToDate(fechaFin);
                        query = String.Format("select count(l.status) as count, l.plcId, l.status, p.name from logs l join plcs p on p.id = l.plcId where time between '{0}' and '{1}' group by l.plcId, l.status;", fechaInicio, fechaFin);
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
                                list.Add(new LogCount()
                                {
                                    Count = Convert.ToInt32(sqldr["count"]),
                                    PlcId = Convert.ToInt32(sqldr["plcId"]),
                                    Status = sqldr["status"].ToString(),
                                    PlcName = sqldr["name"].ToString(),
                                    Month = 0,
                                    Year = 0,
                                });
                            }
                        }
                    }
                }
            }
            catch (MySqlException e) { Debug.WriteLine("MySqlException ListLogCount", e.Message); throw e; }
            catch (Exception e) { Debug.WriteLine("Exception ListLogCount", e.Message); throw e; }
            return list;
        }
        public static List<LogCount> ListLogCountByMonthWithYear(string year)
        {
            List<LogCount> list = new List<LogCount>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Conection.cn))
                {   
                    string query = "";
                    if (String.IsNullOrEmpty(year))
                    {
                        query = "select count(l.status) as count, l.plcId, l.status, p.name, MONTH(l.time) as month, YEAR(l.time) as year from logs l join plcs p on p.id = l.plcId group by l.plcId, l.status, MONTH(l.time), YEAR(l.time) order by MONTH(l.time), YEAR(l.time), l.status, l.plcId;";
                    }
                    else
                    {
                        query = String.Format("select count(l.status) as count, l.plcId, l.status, p.name, MONTH(l.time) as month, YEAR(l.time) as year from logs l join plcs p on p.id = l.plcId where YEAR(l.time) = '{0}' group by l.plcId, l.status, MONTH(l.time), YEAR(l.time) order by MONTH(l.time), YEAR(l.time), l.status, l.plcId;", year);
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
                                list.Add(new LogCount()
                                {
                                    Count = Convert.ToInt32(sqldr["count"]),
                                    PlcId = Convert.ToInt32(sqldr["plcId"]),
                                    Status = sqldr["status"].ToString(),
                                    PlcName = sqldr["name"].ToString(),
                                    Month = Convert.ToInt32(sqldr["month"]),
                                    Year = Convert.ToInt32(sqldr["year"]),
                                });
                            }
                        }
                    }
                }
            }
            catch (MySqlException e) { Debug.WriteLine("MySqlException ListLogCountByMonthWithYear", e.Message); throw e; }
            catch (Exception e) { Debug.WriteLine("Exception ListLogCountByMonthWithYear", e.Message); throw e; }
            return list;
        }
        public static List<int> GetLogYears()
        {
            List<int> list = new List<int>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Conection.cn))
                {
                    string query = "select distinct YEAR(time) as year from logs order by YEAR(time) desc;";
                    Debug.WriteLine("QUERY GetLogYears ", query);
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        connection.Open();
                        using (MySqlDataReader sqldr = cmd.ExecuteReader())
                        {
                            while (sqldr.Read())
                            {
                                list.Add(Convert.ToInt32(sqldr["year"]));
                            }
                        }
                    }
                }
            }
            catch (MySqlException e) { Debug.WriteLine("MySqlException GetLogYears", e.Message); throw e; }
            catch (Exception e) { Debug.WriteLine("Exception GetLogYears", e.Message); throw e; }
            return list;
        }
        public static bool InsertOverbakeLog(OverbakeLog log, out string message)
        {
            bool excecuted = false;
            message = "";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Conection.cn))
                {
                    string query = String.Format("insert into logs (time, plcId, serialNumber, status, message) values ('{0}', {1}, '{2}', '{3}', '{4}');", log.Time, log.PlcId, log.SerialNumber, log.Status, log.Message);
                    Debug.WriteLine("QUERY InsertOverbakeLog ", query);
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
            catch (Exception e) { Debug.WriteLine("Exception InsertOverbakeLog", e.Message); throw e; }
            return excecuted;
        }
        public static bool UpdateOverbakeLog(OverbakeLog log, out string message)
        {
            bool excecuted = false;
            message = "";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Conection.cn))
                {
                    string query = String.Format("update logs set time = '{0}', plcId = {1}, serialNumber = '{2}', status = {3}, message = '{4}' where id = {5};", log.Time, log.PlcId, log.SerialNumber, log.Status, log.Message, log.Id);
                    Debug.WriteLine("QUERY UpdateOverbakeLog ", query);
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
            catch (MySqlException e) { Debug.WriteLine("MySqlException UpdateOverbakeLog", e.Message); throw e; }
            catch (Exception e) { Debug.WriteLine("Exception UpdateOverbakeLog", e.Message); throw e; }
            return excecuted;
        }
        public static bool DeleteOverbakeLog(OverbakeLog log, out string message)
        {
            bool excecuted = false;
            message = "";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Conection.cn))
                {
                    string query = String.Format("delete from logs where id = {0};", log.Id);
                    Debug.WriteLine("QUERY DeleteOverbakeLog ", query);
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
            catch (MySqlException e) { Debug.WriteLine("MySqlException DeleteOverbakeLog", e.Message); throw e; }
            catch (Exception e) { Debug.WriteLine("Exception DeleteOverbakeLog", e.Message); throw e; }
            return excecuted;
        }
    }
}
