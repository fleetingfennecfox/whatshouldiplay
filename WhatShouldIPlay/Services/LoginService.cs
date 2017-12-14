using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WhatShouldIPlay.Models.Domain;

namespace WhatShouldIPlay.Services
{
    public class LoginService
    {
        public bool Login(LoginUser model)
        {
            if (model.Email == "email@email.com" && model.Password == "password")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<LoginUser> SelectAll()
        {
            List<LoginUser> userList = new List<LoginUser>();

            string sqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("dbo.Users_SelectAll", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        LoginUser model = Mapper(reader);
                        userList.Add(model);
                    }
                }

                conn.Close();
            }

            return userList;
        }

        private LoginUser Mapper(SqlDataReader reader)
        {
            LoginUser model = new LoginUser();
            int index = 0;

            model.Id = reader.GetInt32(index++);
            model.Email = reader.GetString(index++);
            model.Password = reader.GetString(index++);

            //For nullable fields
            //if (!reader.IsDBNull(index))
            //{
            //    model.MiddleInitial = reader.GetString(index++);
            //}
            //else
            //{
            //    index++;
            //}

            return model;
        }
    }
}