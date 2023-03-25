using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using MySql.Data.MySqlClient;

namespace DataLayer
{
    internal class ExportApi
    {
        public static DataTable listUsers()
        {
            DataTable dataTable = new DataTable("Grid");
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Conection.cn))
                {
                    string query = "select * from users;";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        connection.Open();
                        using (MySqlDataReader sqldr = cmd.ExecuteReader())
                        {
                            dataTable.Load(sqldr);
                        }
                    }
                }
            }
            catch (MySqlException e) { Debug.WriteLine("MySqlException listUsers", e.Message); throw e; }
            catch (Exception e) { Debug.WriteLine(e.Message); throw e; }
            return dataTable;
        }
    }
}
