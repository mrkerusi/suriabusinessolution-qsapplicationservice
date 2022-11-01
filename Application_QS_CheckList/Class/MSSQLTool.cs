using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Application_QS_CheckList.Class
{
    public static class MSSQLTool
    {
        public static string GetSQLVersion(string ConnectionStr)
        {
            SqlConnection _SqlConnection = null;
            SqlCommand _SqlCommand = null;
            SqlParameter _SqlParameter = null;
            object obj = null;
            string Result = string.Empty;

            try
            {
                _SqlConnection = new SqlConnection();

                _SqlConnection.ConnectionString = ConnectionStr;

                if (_SqlConnection.State != ConnectionState.Open)
                {
                    _SqlConnection.Open();

                }


                _SqlCommand = new SqlCommand();
                _SqlCommand.Connection = _SqlConnection;

                #region CMD TEXT
                _SqlCommand.CommandType = CommandType.Text;
                _SqlCommand.CommandText = @"SELECT @@VERSION";

                #endregion

               

                obj = _SqlCommand.ExecuteScalar();

                if (obj != null && obj != DBNull.Value)
                {
                    Result = obj.ToString();
                }
            }
            catch (Exception ex)
            {
                
               Result = "NA";
            }
            finally
            {
                if (_SqlConnection != null)
                {
                    _SqlConnection.Close();
                    _SqlConnection.Dispose();
                    _SqlConnection = null;
                }

                if (_SqlCommand != null)
                {
                    _SqlCommand.Parameters.Clear();
                    _SqlCommand.Dispose();
                    _SqlCommand = null;
                }

                _SqlParameter = null;
            }

            return Result;
        }

        public static Boolean TestConnection(string ConnectionStr)
        {
            SqlConnection _SqlConnection = null;

            _SqlConnection = new SqlConnection();

            _SqlConnection.ConnectionString = ConnectionStr;

            if (_SqlConnection.State != ConnectionState.Open)
            {
                _SqlConnection.Open();

                return true;
            }

            return false;
        }

    }
}
