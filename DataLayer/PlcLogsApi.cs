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
    public class PlcLogsApi
    {
        private static string FormatToDate(string dateString)
        {
            DateTime date = Convert.ToDateTime(dateString);
            return date.ToString("yyyy-MM-dd");
        }
        public static List<PlcLog> ListLogs(string fechaInicio="", string fechaFin="")
        {
            List<PlcLog> list = new List<PlcLog>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Conection.cn))
                {
                    string query = "";
                    // Debug.WriteLine("On API inicio: ", String.IsNullOrEmpty(fechaInicio).ToString());
                    // Debug.WriteLine("On API fin: ", String.IsNullOrEmpty(fechaFin).ToString());
                    if (String.IsNullOrEmpty(fechaInicio) || String.IsNullOrEmpty(fechaFin))
                    {
                        query = "select pl.id, pl.time, p.name, pl.message from plclogs pl join plcs p on p.id = pl.plcId;";
                    }
                    else
                    {
                        fechaInicio = FormatToDate(fechaInicio);
                        fechaFin = FormatToDate(fechaFin);
                        query = String.Format("select pl.id, pl.time, p.name, pl.message from plclogs pl join plcs p on p.id = pl.plcId where pl.time between'{0}' and '{1}';", fechaInicio, fechaFin);
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
    }
}
