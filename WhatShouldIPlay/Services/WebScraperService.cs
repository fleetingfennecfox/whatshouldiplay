using HtmlAgilityPack;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using WhatShouldIPlay.Models.Domain;

namespace WhatShouldIPlay.Services
{
    public class WebScraperService
    {
        public List<ScrapedPost> GetAll()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            List<ScrapedPost> postsList = new List<ScrapedPost>();

            string url = "https://www.reddit.com/r/Games/search?q=&sort=top&restrict_sr=on&t=day";

            var htmlWeb = new HtmlWeb();
            HtmlDocument document = null;
            document = htmlWeb.Load(url);

            var anchorTags = document.DocumentNode.Descendants("a")
                .Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("search-title"));

            foreach (var node in anchorTags)
            {
                ScrapedPost item = new ScrapedPost();
                item.Title = node.InnerText;
                item.URL = node.GetAttributeValue("href", null);
                postsList.Add(item);
            }
            return postsList;
        }

        public int SavePost(ScrapedPost model)
        {
            int id = 0;

            string sqlConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Posts_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Title", model.Title);
                    cmd.Parameters.AddWithValue("@Url", model.URL);

                    SqlParameter param = new SqlParameter("@Id", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(param);

                    cmd.ExecuteNonQuery();

                    id = (int)cmd.Parameters["@Id"].Value;
                }
                conn.Close();
            }
            return id;
        }
    }
}