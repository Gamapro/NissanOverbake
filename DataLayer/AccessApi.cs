using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;

namespace DataLayer
{
    public class AccessApi
    {
        private static bool ValidateLoginValues(User user, out string message)
        {
            message = "";
            if (user.Username == "" || user.Username == null) { message += "Invalid Username"; return false; }
            if (user.Password == "" || user.Password == null) { message += "Invalid password"; return false; }
            return true;
        }
        public static bool ValidateLogin(User user, out string message, out string token)
        {
            message = "";
            token = "";
            if (!ValidateLoginValues(user, out message)) return false;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Conection.cn))
                {
                    string query = string.Format("select user, password, token from users u where u.user = '{0}' and u.password = '{1}';", user.Username, user.Password);
                    Debug.WriteLine(query);
                    using (MySqlCommand cmd
                        = new MySqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        connection.Open();
                        using (MySqlDataReader sqldr = cmd.ExecuteReader())
                        {
                            while (sqldr.Read())
                            {
                                if (user.Username == Convert.ToString(sqldr["User"]) && user.Password == Convert.ToString(sqldr["Password"]))
                                {
                                    token = Convert.ToString(sqldr["Token"]);
                                    message = "User founded!";
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            catch (MySqlException e) { Debug.WriteLine("MySqlException ValidateLogin", e.Message); throw e; }
            catch (Exception e) { Debug.WriteLine(e.Message); throw e; }
            message = "Invalid credentials";
            return false;
        }

    }
}
