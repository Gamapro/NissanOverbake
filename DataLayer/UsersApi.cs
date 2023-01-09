using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using EntityLayer;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace DataLayer
{
    public class UsersApi
    {
        public static List<User> listUsers()
        {
            List<User> list = new List<User>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Conection.cn))
                {
                    string query = "select * from users u join usertypes ut where u.UserTypeId = ut.id;";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.CommandType = CommandType.Text;
                    connection.Open();
                    using (MySqlDataReader sqldr =  cmd.ExecuteReader())
                    {
                        while (sqldr.Read())
                        {
                            list.Add(new User()
                            {
                                Id = Convert.ToInt32(sqldr["Id"]),
                                Username = Convert.ToString(sqldr["User"]),
                                Password = Convert.ToString(sqldr["Password"]),
                                Name = Convert.ToString(sqldr["Name"]),
                                LastName = Convert.ToString(sqldr["LastName"]),
                                Token = Convert.ToString(sqldr["Token"]),
                                UserTypeId = new UserType()
                                {
                                    Id = Convert.ToInt32(sqldr["UserTypeId"]),
                                    Usertype = Convert.ToString(sqldr["UserType"])
                                }
                            }) ;
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                Debug.WriteLine("MySqlException", e.Message);
                throw e;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw e;
            }
            return list;
        } 

    }
}
