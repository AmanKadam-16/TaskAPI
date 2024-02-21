using Task_API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Task_API.Models
{
    public class TaskModel
    {
        public int ID { get; set; }

        public int TaskSubjectId{ get; set; }

        public string TaskName { get; set; }

        public string Tasktime { get; set; }

        public int TaskTypeId{ get; set; }

        public string IsReminder { get; set; }

        public string TaskSubjectName { get; set; }

        public string TaskTypeName { get; set; }

        public List<TaskModel> AddTasks()
        {

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection oConnection = new SqlConnection(connectionString))
            {
                oConnection.Open();
                using (SqlCommand oCommand = oConnection.CreateCommand())
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.CommandText = "USP_AddTasks";
                    oCommand.Parameters.Add(new SqlParameter("@TaskSubjectId", SqlDbType.Int))
                    .Value = TaskSubjectId;
                    oCommand.Parameters.Add(new SqlParameter("@TaskName", SqlDbType.VarChar))
                    .Value = TaskName;
                    oCommand.Parameters.Add(new SqlParameter("@Tasktime", SqlDbType.DateTime))
                    .Value = Tasktime;
                    oCommand.Parameters.Add(new SqlParameter("@TaskTypeId", SqlDbType.Int))
                    .Value = TaskTypeId;
                    oCommand.Parameters.Add(new SqlParameter("@IsReminder", SqlDbType.Bit))
                    .Value = IsReminder;
 
                    try
                    {
                        oCommand.ExecuteNonQuery();

                    }
                    catch (Exception e)
                    {
                        oConnection.Close();

                    }
                }
            }
            List<TaskModel> tasks = GetTasksList();
            return tasks;
        }



        /*  Deleting tasks */  

        public string DeleteTasks()
        {
            string DeleteTaskReturn = "";
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection oConnection = new SqlConnection(connectionString))
            {
                try
                {
                    oConnection.Open();
                    using (SqlCommand oCommand = oConnection.CreateCommand())
                    {
                        oCommand.CommandType = CommandType.StoredProcedure;
                        oCommand.CommandText = "USP_DeleteTasks";

                        SqlParameter param;
                        param = oCommand.Parameters.Add("@ID", SqlDbType.Int);
                        param.Value = ID;

                        oCommand.ExecuteNonQuery();

                        DeleteTaskReturn = "Task Details Deleted Successfully";
                    }
                }
                catch (Exception e)
                {
                    oConnection.Close();
                    DeleteTaskReturn = "Failed to Delete Task Details";
                }

                return DeleteTaskReturn;
            }
        }



        /*  Displaying Task Details According to ID */
        public TaskModel GetTaskDetails()
        {
            TaskModel TaskDetailsModel = new TaskModel();

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                Connection.Open();
                using (SqlCommand Command = Connection.CreateCommand())
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandText = "Usp_GetTasks";

                    Command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int))
                       .Value = ID;


                    try
                    {
                        SqlDataReader dr = Command.ExecuteReader();
                        while (dr.Read())
                        {
                            TaskDetailsModel =
                                new TaskModel
                                {

                                    TaskSubjectName = dr["TaskSubjectName"].ToString(),
                                    TaskName = dr["TaskName"].ToString(),
                                    Tasktime = dr["Tasktime"].ToString(),
                                    TaskTypeName = dr["TaskTypeName"].ToString()
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

            return TaskDetailsModel;
        }




        /* Get Tasks List */
        public List<TaskModel> GetTasksList()
        {
            List<TaskModel> TaskModel = new List<TaskModel>();
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection oConnection = new SqlConnection(connectionString))
            {
                oConnection.Open();
                using (SqlCommand oCommand = oConnection.CreateCommand())
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.CommandText = "Usp_GetTasksList";

                    try
                    {

                        SqlDataReader dr = oCommand.ExecuteReader();
                        while (dr.Read())
                        {
                            TaskModel.Add(new TaskModel
                            {

                                TaskSubjectName = dr["TaskSubjectName"].ToString(),
                                TaskName = dr["TaskName"].ToString(),
                                Tasktime = dr["Tasktime"].ToString(),
                                TaskTypeName = dr["TaskTypeName"].ToString()
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
                return TaskModel;

            }

        }


        /*  Displaying Task Subjects According to ID */
        public TaskModel GetTaskSubjects()
        {
            TaskModel TaskSubjectsModel = new TaskModel();

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                Connection.Open();
                using (SqlCommand Command = Connection.CreateCommand())
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandText = "Usp_GetTaskSubjects";

                    Command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int))
                       .Value = ID;


                    try
                    {
                        SqlDataReader dr = Command.ExecuteReader();
                        while (dr.Read())
                        {
                            TaskSubjectsModel =
                                new TaskModel
                                {

                                    ID = Convert.ToInt32(dr["ID"].ToString()),
                                    TaskSubjectName = dr["TaskSubjectName"].ToString(),
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

            return TaskSubjectsModel;
        }


        /*  Displaying Task Type According to ID */
        public TaskModel GetTaskType()
        {
            TaskModel TaskTypeModel = new TaskModel();

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                Connection.Open();
                using (SqlCommand Command = Connection.CreateCommand())
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandText = "Usp_GetTaskType";

                    Command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int))
                       .Value = ID;


                    try
                    {
                        SqlDataReader dr = Command.ExecuteReader();
                        while (dr.Read())
                        {
                            TaskTypeModel =
                                new TaskModel
                                {

                                    ID = Convert.ToInt32(dr["ID"].ToString()),
                                    TaskTypeName = dr["TaskTypeName"].ToString(),
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

            return TaskTypeModel;
        }

        /* Update / Edit Task Details */
        public TaskModel EditTaskDetails()
        {
   
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                Connection.Open();
                using (SqlCommand Command = Connection.CreateCommand())
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandText = "Usp_UpdateTasks";

                    SqlParameter param;
                    param = Command.Parameters.Add("ID", SqlDbType.Int);
                    param.Value = ID;
                    param = Command.Parameters.Add("TaskSubjectId", SqlDbType.Int);
                    param.Value = TaskSubjectId;
                    param = Command.Parameters.Add("TaskName", SqlDbType.VarChar);
                    param.Value = TaskName;
                    param = Command.Parameters.Add("Tasktime", SqlDbType.DateTime);
                    param.Value = Tasktime;
                    param = Command.Parameters.Add("TaskTypeId", SqlDbType.Int);
                    param.Value = TaskTypeId;

                    try
                    {
                        Command.ExecuteNonQuery();

                    }
                    catch (Exception e)
                    {
                        Connection.Close();

                    }
                }
            }
            TaskModel tasks = GetTaskDetails();
            return tasks;
        }



    }


}
