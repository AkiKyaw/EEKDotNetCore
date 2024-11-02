﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEKDotNetCore.ConsoleApp
{
    public class AdoDotNetExample
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=EEKDotNetCore;User ID=sa;Password=sa@123;";

        public void Read() 
        {
            Console.WriteLine("Connection String: " + _connectionString);
            SqlConnection connection = new SqlConnection(_connectionString);

            Console.WriteLine("Connection Opening...");
            connection.Open();
            Console.WriteLine("Connection opened.");

            string query = @"SELECT [BlogId]
                           ,[BlogTitle]
                          ,[BlogAuthor]
                          ,[BlohContent]
                          ,[DeleteFlag]
                      FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";
            SqlCommand cmd = new SqlCommand(query, connection);
            //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["BlogId"]);
                Console.WriteLine(reader["BlogTitle"]);
                Console.WriteLine(reader["BlogAuthor"]);
                Console.WriteLine(reader["BlohContent"]);
            }

            Console.WriteLine("Connection Closing...");
            connection.Close();
            Console.WriteLine("Connection closed.");

            //foreach (DataRow dr in dt.Rows)
            //{
            //    Console.WriteLine(dr["BlogId"]);
            //    Console.WriteLine(dr["BlogTitle"]);
            //    Console.WriteLine(dr["BlogAuthor"]);
            //    Console.WriteLine(dr["BlohContent"]);
            //    Console.WriteLine(dr["DeleteFlag"]);
            //}
        }

        public void Create() 
        {
            Console.WriteLine("Blog Title :");
            string title = Console.ReadLine();

            Console.WriteLine("Blog Author");
            string author = Console.ReadLine();

            Console.WriteLine("Blog content");
            string content = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = $@"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlohContent]
           ,[DeleteFlag])
            VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlohContent
           ,0)";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlohContent", content);
            //SqlDataAdapter adapter = new SqlDataAdapter(cmd2);
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            //if (result == 1)
            //{
            //    Console.WriteLine("Saving Successful");
            //}
            //else
            //{
            //    Console.WriteLine("Saving Failed");
            //}

            Console.WriteLine(result == 1 ? "Saving Successful" : "Saving Failed");

        }

        public void Edit()
        {
            Console.Write("Blog ID:");
            string id = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlohContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_Blog] where BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();
            
            if(dt.Rows.Count == 0)
            {
                Console.WriteLine("No data found");
                return;
            }

            DataRow dr = dt.Rows[0];
            Console.WriteLine(dr["BlogId"]);
            Console.WriteLine(dr["BlogTitle"]);
            Console.WriteLine(dr["BlogAuthor"]);
            Console.WriteLine(dr["BlohContent"]);
        }

        public void Update()
        {
            Console.WriteLine("Blog ID :");
            string id = Console.ReadLine();

            Console.WriteLine("Blog Title :");
            string title = Console.ReadLine();

            Console.WriteLine("Blog Author");
            string author = Console.ReadLine();

            Console.WriteLine("Blog content");
            string content = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = $@"UPDATE [dbo].[Tbl_Blog]
            SET [BlogTitle] = @BlogTitle
            ,[BlogAuthor] = @BlogAuthor
            ,[BlohContent] = @BlohContent
            ,[DeleteFlag] = 0
            WHERE BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlohContent", content);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            Console.WriteLine(result == 1 ? "Update Successful" : "Update Failed");

        }

        public void Delete()
        {
            Console.Write("Blog ID:");
            string id = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"UPDATE [dbo].[Tbl_Blog]
               SET [DeleteFlag] = 1
             WHERE BlogId = @BlogId";


            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            Console.WriteLine(result == 1 ? "Delete Successful" : "Delete Failed");

        }
    }
}
