/* EmployeeModel.cs*/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace Task_API.Models
{
    public class EmployeeModel
    {
        public int ID { get; set; }
        public string EmployeeName { get; set; }

        public string BirthDate { get; set; }

        public int DesignationId { get; set; }

        public string DesignationName { get; set; }

        public int Gender { get; set; }

        public string EmailId { get; set; }

        public string PhoneNo { get; set; }

        public string AddEmployeeDetails()
        {
            string AddEmployeeDetailsReturn = "";
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"]
            .ConnectionString;
            using (SqlConnection oConnection = new SqlConnection(connectionString))
            {
                oConnection.Open();
                using (SqlCommand oCommand = oConnection.CreateCommand())
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.CommandText = "USP_AddEmployeeDetails";
                    oCommand.Parameters.Add(new SqlParameter("@EmployeeName", SqlDbType.VarChar))
                    .Value = EmployeeName;
                    oCommand.Parameters.Add(new SqlParameter("@BirthDate", SqlDbType.Date))
                    .Value = BirthDate;
                    oCommand.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int))
                    .Value = DesignationId;
                    oCommand.Parameters.Add(new SqlParameter("@Gender", SqlDbType.Int))
                    .Value = Gender;
                    oCommand.Parameters.Add(new SqlParameter("@EmailId", SqlDbType.VarChar))
                    .Value = EmailId;
                    oCommand.Parameters.Add(new SqlParameter("@PhoneNo", SqlDbType.VarChar))
                    .Value = PhoneNo;
                    try
                    {
                        oCommand.ExecuteNonQuery();
                        AddEmployeeDetailsReturn = "Employee Added Successfully";
                    }
                    catch (Exception e)
                    {
                        oConnection.Close();
                        AddEmployeeDetailsReturn = "Failed to Add Employee";
                    }
                }
            }
            return AddEmployeeDetailsReturn;
        }

        public List<EmployeeModel> GetEmployeeList()
        {
            List<EmployeeModel> EmployeeModel = new List<EmployeeModel>();
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection oConnection = new SqlConnection(connectionString))
            {
                oConnection.Open();
                using (SqlCommand oCommand = oConnection.CreateCommand())
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.CommandText = "USP_GetEmployeeList";

                    try
                    {

                        SqlDataReader dr = oCommand.ExecuteReader();
                        while (dr.Read())
                        {
                            EmployeeModel.Add(new EmployeeModel
                            {
                                ID = Convert.ToInt32(dr["ID"].ToString()),
                                EmployeeName = dr["EmployeeName"].ToString(),
                                BirthDate = dr["BirthDate"].ToString(),
                                DesignationName = dr["DesignationName"].ToString(),
                                EmailId = dr["EmailId"].ToString(),
                                PhoneNo = dr["PhoneNo"].ToString(),
                            }
                            );
                        }
                    }
                    catch (Exception e)
                    {
                        oConnection.Close();
                        // Action after the exception is caught
                    }
                }
                return EmployeeModel;

            }

        }

        public EmployeeModel GetEmployeeDetails()
        {
            EmployeeModel EmployeeDetailModel = new EmployeeModel();

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                Connection.Open();
                using (SqlCommand Command = Connection.CreateCommand())
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandText = "USP_GetEmployeeDetails";

                    Command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int))
                       .Value = ID;


                    try
                    {
                        SqlDataReader dr = Command.ExecuteReader();
                        while (dr.Read())
                        {
                            EmployeeDetailModel =
                                new EmployeeModel
                                {

                                    EmployeeName = dr["EmployeeName"].ToString(),
                                    BirthDate = dr["BirthDate"].ToString(),
                                    DesignationName = dr["DesignationName"].ToString(),
                                    Gender = Convert.ToInt32(dr["Gender"].ToString()),
                                    EmailId = dr["EmailId"].ToString(),
                                    PhoneNo = dr["PhoneNo"].ToString(),
                                };
                        }
                    }
                    catch (Exception e)
                    {
                        Connection.Close();
                        // Action after the exception is caught.
                    }
                }
            }

            return EmployeeDetailModel;
        }

        public string DeleteEmployee()
        {
            string DeleteEmployeeReturn = "";
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection oConnection = new SqlConnection(connectionString))
            {
                try
                {
                    oConnection.Open();
                    using (SqlCommand oCommand = oConnection.CreateCommand())
                    {
                        oCommand.CommandType = CommandType.StoredProcedure;
                        oCommand.CommandText = "USP_DeleteEmployee";

                        SqlParameter param;
                        param = oCommand.Parameters.Add("@ID", SqlDbType.Int);
                        param.Value = ID;

                        oCommand.ExecuteNonQuery();

                        DeleteEmployeeReturn = "Employee Details Deleted Successfully";
                    }
                }
                catch (Exception e)
                {
                    oConnection.Close();
                    DeleteEmployeeReturn = "Failed to Delete Employee Details";
                }

                return DeleteEmployeeReturn;
            }
        }


        public string EditEmployeeDetails()
        {
            string EditDetails = "";
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                Connection.Open();
                using (SqlCommand Command = Connection.CreateCommand())
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandText = "USP_UpdateEmployeeDetails";

                    SqlParameter param;
                    param = Command.Parameters.Add("ID", SqlDbType.Int);
                    param.Value = ID;
                    param = Command.Parameters.Add("EmployeeName", SqlDbType.VarChar);
                    param.Value = EmployeeName;
                    param = Command.Parameters.Add("BirthDate", SqlDbType.Date);
                    param.Value = BirthDate;
                    param = Command.Parameters.Add("EmailId", SqlDbType.VarChar);
                    param.Value = EmailId;
                    param = Command.Parameters.Add("PhoneNo", SqlDbType.VarChar);
                    param.Value = PhoneNo;

                    try
                    {
                        Command.ExecuteNonQuery();
                        EditDetails = "Employee Details Edited Successfully";
                    }
                    catch (Exception e)
                    {
                        Connection.Close();
                        EditDetails = "Failed to Edit Employee Details";
                    }
                }
            }
            return EditDetails;
        }

        public List<EmployeeModel> GetDesignation()
        {
            List<EmployeeModel> EmployeeModel = new List<EmployeeModel>();

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection oConnection = new SqlConnection(connectionString))
            {
                oConnection.Open();
                using (SqlCommand oCommand = oConnection.CreateCommand())
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.CommandText = "USP_GetDesignation";

                    try
                    {
                        SqlDataReader dr = oCommand.ExecuteReader();
                        while (dr.Read())
                        {
                            EmployeeModel.Add(new EmployeeModel
                            {
                                ID = Convert.ToInt32(dr["ID"].ToString()),
                                DesignationName = dr["DesignationName"].ToString()
                            });
                        }
                    }
                    catch (Exception e)
                    {
                        oConnection.Close();
                        // Action after the exception is caught
                    }
                }
            }

            return EmployeeModel;
        }

    }
}