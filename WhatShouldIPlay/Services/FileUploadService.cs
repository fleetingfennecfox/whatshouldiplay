using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using WhatShouldIPlay.Models.Request;

namespace WhatShouldIPlay.Services
{
    public class FileUploadService
    {
        string sqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public int Insert(UserFile model)
        {
            int res = 0;
            string systemFileName = string.Empty;

            if (model.ByteArray != null)
            {
                systemFileName = string.Format("{0}_{1}{2}",
                    model.UserFileName,
                    Guid.NewGuid().ToString(),
                    model.Extension);

                SaveBytesFile(model.SaveLocation, systemFileName, model.ByteArray);
            }

            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();
                
                using (SqlCommand cmd = new SqlCommand("Files_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserFileName", model.UserFileName);
                    cmd.Parameters.AddWithValue("@SystemFileName", systemFileName);
                    cmd.Parameters.AddWithValue("@Location", model.SaveLocation);
                    cmd.Parameters.AddWithValue("@UserId", model.UserId);

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

        private void SaveBytesFile(string location, string systemFileName, byte[] Bytes)
        {
            string fileBase = "~/images";
            var filePath = HttpContext.Current.Server.MapPath(fileBase + "/" + location + "/" + systemFileName);
            File.WriteAllBytes("C:/repos/github/whatshouldiplay/WhatShouldIPlay/images/" + systemFileName, Bytes);
        }
    }
}