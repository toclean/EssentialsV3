using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer.Models;
using Microsoft.SqlServer.Server;

namespace DataAccessLayer
{
    public class UserFactory
    {
        public IEnumerable<User> GetUser(DataRowCollection rows)
        {
            var users = new List<User>();
            foreach (DataRow row in rows)
            {
                users.Add(new User
                {
                    Username = row["USERNAME"].ToString(),
                    Password = row["PASSWORD"].ToString()
                });
            }

            return users;
        }

        public DataRowCollection GetUser(User user)
        {
            var command = new SqlCommand("SELECT * " +
                                         "FROM USERS " +
                                         "WHERE USERNAME = @USERNAME " +
                                         "AND PASSWORD = @PASSWORD",
                                         DataBase.Connect());
            command.Parameters.AddWithValue("@USERNAME", user.Username);
            command.Parameters.AddWithValue("@PASSWORD", user.Password);
            var table = new DataTable();
            var adapter = new SqlDataAdapter(command);
            adapter.Fill(table);

            return table.Rows;
        }

        public bool IsUser(User user)
        {
            var command = new SqlCommand("SELECT COUNT(*) COUNT " +
                                         "FROM USERS " +
                                         "WHERE USERNAME = @USERNAME",
                                         DataBase.Connect());
            command.Parameters.AddWithValue("@USERNAME", user.Username);
            var table = new DataTable();
            var adapter = new SqlDataAdapter(command);
            adapter.Fill(table);

            return table.Rows.Count > 0;
        }

        public void CreateUser(User user, string settings)
        {
            var command = new SqlCommand("INSERT INTO USERS " +
                                         "(USERID, USERNAME, PASSWORD, DATE, SETTINGS) " +
                                         "VALUES (@USERID, @USERNAME, @PASSWORD, @DATE, @SETTINGS)",
                                         DataBase.Connect());

            command.Parameters.AddWithValue("@USERID", Guid.NewGuid());
            command.Parameters.AddWithValue("@USERNAME", user.Username);
            command.Parameters.AddWithValue("@PASSWORD", user.Password);
            command.Parameters.AddWithValue("@DATE", DateTime.Now);
            command.Parameters.AddWithValue("@SETTINGS", settings);
            command.ExecuteNonQuery();
        }
    }
}
