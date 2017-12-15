using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WhatShouldIPlay.Models.Request;

namespace WhatShouldIPlay.Services
{
    public class GameProfileService
    {
        public int AddGame(GameProfile model)
        {
            int res = 0;

            string sqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("dbo.Games_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Title", model.Title);
                    cmd.Parameters.AddWithValue("@Platforms", model.Platforms);
                    cmd.Parameters.AddWithValue("@Genres", model.Genres);
                    cmd.Parameters.AddWithValue("@Studio", model.Studio);
                    cmd.Parameters.AddWithValue("@Directors", model.Directors);

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

        public List<GameProfile> SelectAll()
        {
            List<GameProfile> gamesList = new List<GameProfile>();

            string sqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("dbo.Games_SelectAll", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        GameProfile model = Mapper(reader);
                        gamesList.Add(model);
                    }
                }

                conn.Close();
            }

            return gamesList;
        }

        public GameProfile SelectById(int id)
        {
            GameProfile game = new GameProfile();

            string sqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("dbo.Games_SelectById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        game = Mapper(reader);
                    }
                }

                conn.Close();
            }

            return game;
        }

        public bool UpdateGame(GameProfile model)
        {
            bool res = false;

            string sqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("dbo.Games_Update", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", model.Id);
                    cmd.Parameters.AddWithValue("@Title", model.Title);
                    cmd.Parameters.AddWithValue("@Platforms", model.Platforms);
                    cmd.Parameters.AddWithValue("@Genres", model.Genres);
                    cmd.Parameters.AddWithValue("@Studio", model.Studio);
                    cmd.Parameters.AddWithValue("@Directors", model.Directors);

                    cmd.ExecuteNonQuery();

                    res = true;
                }

                conn.Close();
            }

            return res;
        }

        public bool DeleteGame(int id)
        {
            bool res = false;

            string sqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("dbo.Games_Delete", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                    res = true;
                }

                conn.Close();
            }

            return res;
        }

        private GameProfile Mapper(SqlDataReader reader)
        {
            GameProfile model = new GameProfile();
            int index = 0;

            model.Id = reader.GetInt32(index++);
            model.Title = reader.GetString(index++);
            model.Platforms = reader.GetInt32(index++);
            model.Genres = reader.GetInt32(index++);
            model.Studio = reader.GetInt32(index++);
            model.Directors = reader.GetInt32(index++);

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