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
    public static class clsCategoryData
    {
        public static bool GetCategoryByID(int CategoryID, ref string CategoryName, ref int? CategoryParentID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection =
                       new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("spu_GetCategoryByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Category_id", CategoryID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Process results
                            while (reader.Read())
                            {
                                isFound = true;
                                CategoryName = (string)reader["Category_name"];


                                if (reader["Category_parent"] != DBNull.Value)
                                {
                                    CategoryParentID = (int)reader["Category_parent"];
                                }
                                else
                                {
                                    CategoryParentID = null;
                                }
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

        public static bool GetCategoryByName(ref int CategoryID, string CategoryName, ref int? CategoryParentID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection =
                       new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("spu_GetCategoryByName", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Category_name", CategoryName);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Process results
                            while (reader.Read())
                            {
                                isFound = true;
                                CategoryID = (int)reader["Category_id"];


                                if (reader["Category_parent"] != DBNull.Value)
                                {
                                    CategoryParentID = (int)reader["Category_parent"];
                                }
                                else
                                {
                                    CategoryParentID = null;
                                }
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

        public static DataTable GetAllCategories()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection =
                       new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("spu_GetAllCategories", connection))
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

        public static int AddNewCategories(string CategoryName, int? CategoryParent)
        {
            int CatrgoryID = -1;

            try
            {
                using (SqlConnection connection =
                       new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("spu_AddNewCategory", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Category_name", CategoryName);

                        if (CategoryParent.HasValue)
                            command.Parameters.AddWithValue("@Category_Parent", CategoryParent);
                        else
                            command.Parameters.AddWithValue("@Category_Parent", System.DBNull.Value);

                        SqlParameter outputIdParam = new SqlParameter("@Category_id", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);

                        command.ExecuteNonQuery();

                        CatrgoryID = (int)command.Parameters["@Category_id"].Value;
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
            }




            return CatrgoryID;
        }

        public static bool UpdateCategory(int CategoryID, string CategoryName, int? CategoryParent) {
            int rowsAffected = 0;

            try
            {
                using(SqlConnection connection =new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("spu_UpdateCategory", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Category_id", CategoryID);
                        command.Parameters.AddWithValue("@Category_name", CategoryName);

                        if (CategoryParent.HasValue)
                            command.Parameters.AddWithValue("@Category_Parent", CategoryParent);
                        else
                            command.Parameters.AddWithValue("@Category_Parent", System.DBNull.Value);

                        rowsAffected = command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex) { return false; }

            return (rowsAffected > 0);
        }

        public static bool IsCategoryExist(string CategoryName)
        {
            
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("spu_CheckCategoryExists", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Category_name",(object)CategoryName??DBNull.Value);

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

        public static bool DeleteCategory(int CategoryID)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection =
                       new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("spu_DeleteCategory", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Category_id", CategoryID);

                        
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

