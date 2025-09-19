using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Generator_Code_DataAccess_Layer
{
    public class clsCodeGeneratorDataAccess_DataAcessLayer
    {
        public static  async Task<DataView> GetAllDataBase()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    SqlCommand command = new SqlCommand("SP_GetAllDataBase", connection);

                    command.CommandType = CommandType.StoredProcedure;

                   await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            dataTable.Load(reader);
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return  dataTable.DefaultView;

        }

        public static async Task<DataView> GetAllTablesFromDataBase(string DataBaseName)
        {
            DataTable dataTable = new DataTable();       
            try
            {
                
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {                 
                    string query = @"SELECT TABLE_NAME 
                                     FROM INFORMATION_SCHEMA.TABLES 
                                     WHERE TABLE_TYPE = 'BASE TABLE ';";

                    SqlCommand command = new SqlCommand(query, connection);


                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            dataTable.Load(reader);
                        }
                    }                    
                }
            }
            catch (Exception ex)
            {

            }           
            return dataTable.DefaultView;
        }

        public static async Task<DataView> GetTableDetailsFromDataBase(string DataBaseName, string TableName)
        {
            DataTable dataTable = new DataTable();
         
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {                   
                    string query = @"SELECT   Table1.COLUMN_NAME, Table1.DATA_TYPE, Table1.CHARACTER_MAXIMUM_LENGTH, Table1.IS_NULLABLE,Table2.CONSTRAINT_NAME
                                     FROM   INFORMATION_SCHEMA.COLUMNS AS Table1 LEFT OUTER JOIN
                                     INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS Table2 ON Table1.TABLE_NAME = Table2.TABLE_NAME AND Table1.COLUMN_NAME = Table2.COLUMN_NAME AND Table2.TABLE_SCHEMA = Table2.TABLE_SCHEMA
                                     WHERE Table1.TABLE_NAME = @TableName ";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TableName", TableName);
                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                dataTable.Load(reader);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
            


            return dataTable.DefaultView;


        }
    }
}
