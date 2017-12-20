using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WhatShouldIPlay.Models.Domain;
using WhatShouldIPlay.Models.Request;

namespace WhatShouldIPlay.Services
{
    public class GameProfileService
    {
        public int AddGame(GameProfileRequest model)
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

        public GameProfileRequest SelectById(int id)
        {
            GameProfileRequest game = new GameProfileRequest();

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
                        int index = 0;

                        game.Id = reader.GetInt32(index++);
                        game.Title = reader.GetString(index++);
                    }
                }

                conn.Close();
            }

            return game;
        }

        public bool UpdateGame(GameProfileRequest model)
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

        public GameAttributes SelectAllAttributes()
        {
            GameAttributes attributes = new GameAttributes();

            string sqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("dbo.Attributes_SelectAll", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    while (reader.Read())
                    {
                        Platforms plats = new Platforms();
                        int index = 0;

                        plats.Id = reader.GetInt32(index++);
                        plats.Platform = reader.GetString(index++);
                        attributes.Platforms.Add(plats);
                    }
                    reader.NextResult();

                    while (reader.Read())
                    {
                        Genres genres = new Genres();
                        int index = 0;

                        genres.Id = reader.GetInt32(index++);
                        genres.Genre = reader.GetString(index++);
                        attributes.Genres.Add(genres);
                    }
                    reader.NextResult();

                    while (reader.Read())
                    {
                        Studios studios = new Studios();
                        int index = 0;

                        studios.Id = reader.GetInt32(index++);
                        studios.Studio = reader.GetString(index++);
                        attributes.Studios.Add(studios);
                    }
                    reader.NextResult();

                    while (reader.Read())
                    {
                        Directors dirs = new Directors();
                        int index = 0;

                        dirs.Id = reader.GetInt32(index++);
                        dirs.Director = reader.GetString(index++);
                        attributes.Directors.Add(dirs);
                    }
                }

                conn.Close();
            }

            return attributes;
        }
    }
}