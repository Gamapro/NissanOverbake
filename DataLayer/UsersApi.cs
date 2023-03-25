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
using System.Xml.Linq;
using Google.Protobuf.WellKnownTypes;
using Org.BouncyCastle.Utilities.Collections;
using Google.Protobuf;
using Renci.SshNet;
using System.Web;

namespace DataLayer
{
    public class UsersApi
    {
        private static bool validateUser(User user, out string message)
        {
            message = "";
            if (user.Token == "" || user.Token == null) { message += "Invalid token "; return false; }
            if (user.Name == "" || user.Name == null) { message += "Invalid First Name "; return false; }
            if (user.LastName == "" || user.LastName == null) { message += "Invalid Last Name "; return false; }
            if (user.Username == "" || user.Username == null) { message += "Invalid Username "; return false; }
            if (user.Password == "" || user.Password == null) { message += "Invalid password "; return false; }
            if (user.UserTypeId.Id != 1 && user.UserTypeId.Id != 2) { message += "Invalid type of user "; return false; }
            if (message != "") return false;
            return true;
        }
        public static List<User> listUsers()
        {
            List<User> list = new List<User>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Conection.cn))
                {
                    string query = "select * from users u join usertypes ut where u.UserTypeId = ut.id;";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        connection.Open();
                        using (MySqlDataReader sqldr = cmd.ExecuteReader())
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
                                });
                            }
                        }
                    }
                }
            }
            catch (MySqlException e) { Debug.WriteLine("MySqlException listUsers", e.Message); throw e; }
            catch (Exception e) { Debug.WriteLine(e.Message); throw e; }
            return list;
        }
        public static bool IsAdmin(string token)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Conection.cn))
                {
                    string query = string.Format("select token, userTypeId from users u where u.token = '{0}';", token);
                    using (MySqlCommand cmd
                        = new MySqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        connection.Open();
                        using (MySqlDataReader sqldr = cmd.ExecuteReader())
                        {
                            while (sqldr.Read())
                            {
                                if (token == Convert.ToString(sqldr["Token"]))
                                {
                                    int userId = Convert.ToInt32(sqldr["UserTypeId"]);
                                    return userId == 2;
                                }
                            }
                        }
                    }
                }
            }
            catch (MySqlException e) { Debug.WriteLine("MySqlException IsAdmin", e.Message); throw e; }
            catch (Exception e) { Debug.WriteLine(e.Message); throw e; }
            return false;
        }
        public static bool UserExists(User user)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Conection.cn))
                {
                    string query = "select * from users u join usertypes ut where u.UserTypeId = ut.id;";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        connection.Open();
                        using (MySqlDataReader sqldr = cmd.ExecuteReader())
                        {
                            while (sqldr.Read())
                            {
                                if( user.Id == Convert.ToInt32(sqldr["Id"]) &&
                                    // user.Username == Convert.ToString(sqldr["User"]) &&
                                    // user.Password == Convert.ToString(sqldr["Password"]) &&
                                    // user.UserTypeId.Id == Convert.ToInt32(sqldr["UserTypeId"])
                                    user.Token == Convert.ToString(sqldr["Token"])
                                )
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            catch (MySqlException e) { Debug.WriteLine("MySqlException listUsers", e.Message); throw e; }
            catch (Exception e) { Debug.WriteLine(e.Message); throw e; }
            return false;
        }
        public static bool RegisterOrUpdateUser(User user, out string message, out string token)
        {
            if (String.IsNullOrEmpty(user.Token)) user.Token = Encryption.GenerateToken();
            token = user.Token;
            if (!UsersApi.validateUser(user, out message)) return false;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Conection.cn))
                {
                    connection.Open();
                    string query = "";
                    if( user.Id == 0) query = string.Format("insert into users(user, password, name, lastName, userTypeId, token) Values ('{0}', '{1}', '{2}', '{3}', {4}, '{5}');", 
                        user.Username, user.Password, user.Name, user.LastName, user.UserTypeId.Id, user.Token);
                    else query = string.Format("update users set user = '{0}', password = '{1}', name = '{2}', lastName = '{3}', userTypeId = {4}, token = '{5}' where id = {6};",
                            user.Username, user.Password, user.Name, user.LastName, user.UserTypeId.Id, user.Token, user.Id);
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException e){ message = "MySqlException"+e.Message; return false; }
            catch (Exception e) { message = "Exception" + e.Message; return false; }
            message = "User inserted succesfully!";
            return true;
        }
        public static bool DeleteUser(User user, string token, out string message)
        {
            message = "";
            if (!UsersApi.UserExists(user))
            {
                message = "User does not exist";
                return false;
            }
            if (user.Token == token)
            {
                message = "Cannot delete current user";
                return false;
            }
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Conection.cn))
                {
                    connection.Open();
                    string query = string.Format("delete from users where id = {0} and token = '{1}';", user.Id, user.Token);
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException e) { message = "MySqlException" + e.Message; return false; }
            catch (Exception e) { message = "Exception" + e.Message; return false; }
            message = "User deleted succesfully!";
            return true;
        }
        public static string GetUserToken(User user)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Conection.cn))
                {
                    string query = "select * from users u join usertypes ut where u.UserTypeId = ut.id;";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        connection.Open();
                        using (MySqlDataReader sqldr = cmd.ExecuteReader())
                        {
                            while (sqldr.Read())
                            {
                                if (user.Id == Convert.ToInt32(sqldr["Id"])
                                    //user.Username == Convert.ToString(sqldr["User"]) &&
                                    //user.Password == Convert.ToString(sqldr["Password"]) &&
                                    //user.UserTypeId.Id == Convert.ToInt32(sqldr["UserTypeId"])
                                )
                                {
                                    return Convert.ToString(sqldr["Token"]);
                                }
                            }
                        }
                    }
                }
            }
            catch (MySqlException e) { Debug.WriteLine("MySqlException listUsers", e.Message); throw e; }
            catch (Exception e) { Debug.WriteLine(e.Message); throw e; }
            return "";
        }
        public static int GetUserId(User user)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Conection.cn))
                {
                    string query = "select * from users u join usertypes ut where u.UserTypeId = ut.id;";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        connection.Open();
                        using (MySqlDataReader sqldr = cmd.ExecuteReader())
                        {
                            while (sqldr.Read())
                            {
                                if (user.Token == Convert.ToString(sqldr["Token"])
                                    //user.Username == Convert.ToString(sqldr["User"]) &&
                                    //user.Password == Convert.ToString(sqldr["Password"]) &&
                                    //user.UserTypeId.Id == Convert.ToInt32(sqldr["UserTypeId"])
                                )
                                {
                                    return Convert.ToInt32(sqldr["Id"]);
                                }
                            }
                        }
                    }
                }
            }
            catch (MySqlException e) { Debug.WriteLine("MySqlException listUsers", e.Message); throw e; }
            catch (Exception e) { Debug.WriteLine(e.Message); throw e; }
            return 0;
        }
    }
}
