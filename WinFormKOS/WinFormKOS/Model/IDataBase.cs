﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormKOS.Model
{
    class IDataBase
    {
        public static string connectionString = "Server=DESKTOP-BHDA7AK\\SQLEXPRESS;Database=KOSDb;Trusted_Connection=True;";

        public static DataTable DataToDataTable(string query, List<SqlParameter> parameters)
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(query, con);
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters.ToArray());
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
            catch (SqlException )
            {
                throw ;
            }
        }

        public static DataTable DataToDataTable(string query, SqlParameter parameter)
        {
            return DataToDataTable(query, new List<SqlParameter>() { parameter });
        }

        public static DataTable DataToDataTable(string query)
        {
            return DataToDataTable(query, new List<SqlParameter>() { });
        }

        public static void executeNonQuery(string query, List<SqlParameter> parameters)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            try
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters.ToArray());
                }
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public static void executeNonQuery(string query, SqlParameter parameter)
        {
            executeNonQuery(query, new List<SqlParameter>() { parameter });
        }

        public static object executeScalar(string query, List<SqlParameter> parameters)
        {
            object value = null;

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            try
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters.ToArray());
                }
                value = cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }

            return value;
        }
    }
}
