using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_System_DataAccess
{
    public class clsBrandData
    {
        public static bool GetBrandByID(int BrandID, ref string BrandName)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection =
                       new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("spu_GetBrandByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Brand_id", BrandID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Process results
                            while (reader.Read())
                            {
                                isFound = true;
                                BrandName = (string)reader["brand_name"];
                            }
                        }
                    }
                }
            }
            catch
            {
                return false;
            }

            return isFound;
        }

        public static bool GetBrandByName(ref int BrandID, string BrandName)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection =
                       new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("spu_GetBrandByName", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Brand_name", BrandName);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                isFound = true;
                                BrandID = (int)reader["brand_id"];

                            }
                        }
                    }
                }
            }
            catch
            {
                return false;
            }

            return isFound;
        }

        public static DataTable GetAllBrands()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection =
                       new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("spu_GetAllBrands", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
            }

            return dt;
        }

        public static int AddNewbrand(string BrandName)
        {
            int BrandID = -1;

            try
            {
                using (SqlConnection connection =
                       new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("spu_AddNewBrand", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Brand_name", BrandName);

                        SqlParameter outputIdParam = new SqlParameter("@Brand_id", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);

                        command.ExecuteNonQuery();

                        BrandID = (int)command.Parameters["@Brand_id"].Value;
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
            }




            return BrandID;
        }

        public static bool UpdateBrand(int BrandID, string BrandName)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("spu_UpdateBrand", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Brand_id", BrandID);
                        command.Parameters.AddWithValue("@Brand_name", BrandName);


                        rowsAffected = command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex) { return false; }

            return (rowsAffected > 0);
        }

        public static bool IsBrandExist(string BrandName)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("spu_CheckBrandExists", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Brand_name", (object)BrandName ?? DBNull.Value);

                        SqlParameter returnParameter = new SqlParameter("@ReturnVal", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };

                        command.Parameters.Add(returnParameter);
                        command.ExecuteNonQuery();

                        int result = (int)returnParameter.Value;


                        return (result == 1);
                    }

                }
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteBrand(int BrandID)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection =
                       new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("spu_DeleteBrand", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Brand_id", BrandID);


                        rowsAffected = command.ExecuteNonQuery();

                    }
                }
            }
            catch
            {
                return false;
            }
            return (rowsAffected > 0);

        }
    }
}
