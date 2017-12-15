using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WhatShouldIPlay.Models.Domain;

namespace WhatShouldIPlay.Services
{
    public class LoginService
    {
        public bool Login(User model)
        {
            bool res = false;

            string sqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("dbo.Users_SelectByEmail", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        User responseModel = Mapper(reader);

                        int multOf4 = responseModel.Salt.Length % 4;
                        if (multOf4 > 0)
                        {
                            responseModel.Salt += new string('=', 4 - multOf4);
                        }
                        CryptographyService cryptSvc = new CryptographyService();
                        string passwordHash = cryptSvc.Hash(model.BasicPass, responseModel.Salt);

                        if (passwordHash == responseModel.EncryptedPass)
                        {
                            res = true;
                        }
                    }
                }

                conn.Close();
            }

            return res;
        }

        private User Mapper(SqlDataReader reader)
        {
            User model = new User();
            int index = 0;

            model.Id = reader.GetInt32(index++);
            model.Email = reader.GetString(index++);
            model.BasicPass = reader.GetString(index++);
            model.EncryptedPass = reader.GetString(index++);
            model.Salt = reader.GetString(index++);

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