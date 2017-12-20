using System;
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

                #region Comments
                /*
                 * back up in case table insert doesnt work
                using (SqlCommand cmd = new SqlCommand("dbo.Games_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Title", model.Title);

                    SqlParameter param = new SqlParameter("@Id", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(param);

                    cmd.ExecuteNonQuery();

                    res = (int)cmd.Parameters["@Id"].Value;
                }

                for (int i = 0; i < model.Platforms.Length; i++)
                {
                    using (SqlCommand cmd = new SqlCommand("dbo.GamesPlatforms_Insert", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@GameId", res);
                        cmd.Parameters.AddWithValue("@PlatformId", model.Platforms[i]);

                        cmd.ExecuteNonQuery();
                    }
                }
                */
                #endregion Comments

                using (SqlCommand cmd = new SqlCommand("dbo.Games_InsertWithAttr", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Title", model.Title);

                    using (var table = new DataTable())
                    {
                        table.Columns.Add("PlatformId", typeof(int));

                        for (int i = 0; i < model.Platforms.Length; i++)
                            table.Rows.Add(model.Platforms[i]);

                        var pList = new SqlParameter("@Platforms", SqlDbType.Structured);
                        pList.TypeName = "PlatformsTemp";
                        pList.Value = table;

                        cmd.Parameters.Add(pList);
                    }

                    using (var table = new DataTable())
                    {
                        table.Columns.Add("GenreId", typeof(int));

                        for (int i = 0; i < model.Genres.Length; i++)
                            table.Rows.Add(model.Genres[i]);

                        var pList = new SqlParameter("@Genres", SqlDbType.Structured);
                        pList.TypeName = "GenresTemp";
                        pList.Value = table;

                        cmd.Parameters.Add(pList);
                    }

                    cmd.Parameters.AddWithValue("@Studio", model.Studio);

                    using (var table = new DataTable())
                    {
                        table.Columns.Add("DirectorId", typeof(int));

                        for (int i = 0; i < model.Directors.Length; i++)
                            table.Rows.Add(model.Directors[i]);

                        var pList = new SqlParameter("@Directors", SqlDbType.Structured);
                        pList.TypeName = "DirectorsTemp";
                        pList.Value = table;

                        cmd.Parameters.Add(pList);
                    }

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

                using (SqlCommand cmd = new SqlCommand("dbo.Games_SelectByIdWithDetails", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int index = 0;

                        game.Id = id;
                        game.Title = reader.GetString(index++);
                    }
                    reader.NextResult();

                    while (reader.Read())
                    {
                        Platforms plats = new Platforms();
                        int index = 0;

                        plats.Id = reader.GetInt32(index++);
                        plats.Platform = reader.GetString(index++);
                        game.Platforms.Add(plats);
                    }
                    reader.NextResult();

                    while (reader.Read())
                    {
                        Genres genres = new Genres();
                        int index = 0;

                        genres.Id = reader.GetInt32(index++);
                        genres.Genre = reader.GetString(index++);
                        game.Genres.Add(genres);
                    }
                    reader.NextResult();

                    while (reader.Read())
                    {
                        int index = 0;

                        game.Studios.Id = reader.GetInt32(index++);
                        game.Studios.Studio = reader.GetString(index++);
                    }
                    reader.NextResult();

                    while (reader.Read())
                    {
                        Directors dirs = new Directors();
                        int index = 0;

                        dirs.Id = reader.GetInt32(index++);
                        dirs.Director = reader.GetString(index++);
                        game.Directors.Add(dirs);
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

                using (SqlCommand cmd = new SqlCommand("dbo.Games_UpdateWithAttr", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", model.Id);
                    cmd.Parameters.AddWithValue("@Title", model.Title);

                    using (var table = new DataTable())
                    {
                        table.Columns.Add("PlatformId", typeof(int));

                        for (int i = 0; i < model.Platforms.Length; i++)
                            table.Rows.Add(model.Platforms[i]);

                        var pList = new SqlParameter("@Platforms", SqlDbType.Structured);
                        pList.TypeName = "PlatformsTemp";
                        pList.Value = table;

                        cmd.Parameters.Add(pList);
                    }

                    using (var table = new DataTable())
                    {
                        table.Columns.Add("GenreId", typeof(int));

                        for (int i = 0; i < model.Genres.Length; i++)
                            table.Rows.Add(model.Genres[i]);

                        var pList = new SqlParameter("@Genres", SqlDbType.Structured);
                        pList.TypeName = "GenresTemp";
                        pList.Value = table;

                        cmd.Parameters.Add(pList);
                    }

                    cmd.Parameters.AddWithValue("@Studio", model.Studio);

                    using (var table = new DataTable())
                    {
                        table.Columns.Add("DirectorId", typeof(int));

                        for (int i = 0; i < model.Directors.Length; i++)
                            table.Rows.Add(model.Directors[i]);

                        var pList = new SqlParameter("@Directors", SqlDbType.Structured);
                        pList.TypeName = "DirectorsTemp";
                        pList.Value = table;

                        cmd.Parameters.Add(pList);
                    }

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

                using (SqlCommand cmd = new SqlCommand("dbo.Games_DeleteWithAttr", conn))
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