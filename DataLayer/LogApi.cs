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
        public static List<Log> listLogs()
        {
            List<Log> list = new List<Log>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Conection.cn))
                {
                    string query = "select * from logs;";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        connection.Open();
                        using (MySqlDataReader sqldr = cmd.ExecuteReader())
                        {
                            while (sqldr.Read())
                            {
                                list.Add(new Log()
                                {
                                    Id = Convert.ToInt32(sqldr["id"]),
                                    Time = Convert.ToString(sqldr["time"]),
                                    PlcId = Convert.ToInt32(sqldr["plcId"]),
                                    Message = sqldr["message"].ToString(),
                                    Status = sqldr["status"].ToString(),
                                    SerialNumber = sqldr["serialNumber"].ToString()
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
