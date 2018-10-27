using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Collections;

namespace ToDo.ViewModels
{
    class DB_API
    {
       NpgsqlConnection conn;

            public void start()
            {
                conn = new NpgsqlConnection("Server=127.0.0.1;Port=5433;User Id=postgres;Password=database;Database=ToDo;");
            }

            public void insert(string query)
            {
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand(query, conn);
                int rowsaffected;
                try
                {
                    rowsaffected = command.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            }

            public void update(string query)
            {
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand(query, conn);
                int rowsaffected;
                try
                {
                    rowsaffected = command.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            }

            public ArrayList select(string query)
            {
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand(query, conn);

                ArrayList array = new ArrayList();
                try
                {
                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            array.Add(dr[i]);
                        }
                    }
                }
                finally
                {
                    conn.Close();
                }

                return array;
            }

            public void delete(string query)
            {
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand(query, conn);
                int rowsaffected;
                try
                {
                    rowsaffected = command.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            }
        
    }

}