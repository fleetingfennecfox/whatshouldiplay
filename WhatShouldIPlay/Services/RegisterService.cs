using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WhatShouldIPlay.Models.Domain;

namespace WhatShouldIPlay.Services
{
    public class RegisterService
    {
        public int Register(User model)
        {
            int res = 0;

            CryptographyService cryptSvc = new CryptographyService();

            model.Salt = cryptSvc.GenerateRandomString();
            model.EncryptedPass = cryptSvc.Hash(model.BasicPass, model.Salt);

            string sqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("dbo.Users_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@EncryptedPass", model.EncryptedPass);
                    cmd.Parameters.AddWithValue("@Salt", model.Salt);

                    SqlParameter param = new SqlParameter("@Id", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(param);

                    cmd.ExecuteNonQuery();

                    res = (int)cmd.Parameters["@Id"].Value;
                }

                conn.Close();
            }

            return res;
        }
    }
}