using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAAutoFramework.Helpers
{
    //EXTENSION CLASS RULES TO BE FOLLOWED
    //1.MAKE CLASS AS STATIC
    //2.MAKE METHOSDS AS STATIC
    //3.FIRST PARAMETER OF METHODS SHOULD BE THE TYPE FOLLOWED BY "THIS"
    public static class DataHelperExtensions
    {
        //open the connection
        public static SqlConnection DBConnect(this SqlConnection sqlConnection, string connectionString)
        {
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                return sqlConnection;

            }
            catch (Exception e)
            {
                LogHelper.Write("Error : " + e.Message);
            }
            return null;
        }


        //close the connection
        public static void DBClose(this SqlConnection sqlConnection)
        {
            try
            {
                sqlConnection.Close();
            }
            catch (Exception e)
            {
                LogHelper.Write("Error: " + e.Message);
            }
        }

        //execution
        public static DataSet ExecuteQuery(this SqlConnection sqlConnection, string queryString)
        {
            DataSet dataSet;
            try
            {

                if (sqlConnection == null || (sqlConnection != null && (sqlConnection.State == ConnectionState.Closed ||
                    sqlConnection.State == ConnectionState.Broken)))
                {
                    sqlConnection.Open();
                }
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = new SqlCommand(queryString, sqlConnection);
                dataAdapter.SelectCommand.CommandType = CommandType.Text;

                dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "table");
                sqlConnection.Close();
                return dataSet/*.Tables["table"]*/;
            }
            catch (Exception e)
            {
                dataSet = null;
                sqlConnection.Close();
                return null;
                LogHelper.Write("Error: " + e.Message);
            }
            finally
            {
                sqlConnection.Close();
                dataSet = null;
            }
        }


    }
}
