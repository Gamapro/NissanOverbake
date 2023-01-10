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
        public static List<PlcLog> listLogs()
        {
            List<PlcLog> list = new List<PlcLog>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Conection.cn))
                {
                    string query = "select * from plcLogs pl join plcs p on p.id = pl.plcId; ";
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
                                    PlcId = Convert.ToInt32(sqldr["plcId"]),
                                    Message = sqldr["message"].ToString(),
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
