using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Task_API.Models;

namespace Task_API.Controllers
{
    public class IndexController : ApiController
    {
        [HttpPost]
        [Route("AddEmployeeDetails")]
        public string AddEmployeeDetails([FromBody] EmployeeModel employeeModel)
        {
            return employeeModel.AddEmployeeDetails();
        }

        [HttpGet]
        [Route("GetEmployeeList")]
        public List<EmployeeModel> GetEmployeeList([FromBody] EmployeeModel employeeModel)
        {
            if (employeeModel == null)
                employeeModel = new EmployeeModel();
            return employeeModel.GetEmployeeList();
        }

        [HttpPost]
        [Route("GetEmployeeDetails")]
        public EmployeeModel GetEmployeeDetails([FromBody] EmployeeModel employeeModel)
        {
            return employeeModel.GetEmployeeDetails();
        }

        [HttpPost]
        [Route("DeleteEmployee")]
        public string DeleteEmployee([FromBody] EmployeeModel employeeModel)
        {
            return employeeModel.DeleteEmployee();
        }

        [HttpPost]
        [Route("EditEmployeeDetails")]
        public string EditEmployeeDetails([FromBody] EmployeeModel employeeModel)
        {
            return employeeModel.EditEmployeeDetails();
        }

        [HttpPost]
        [Route("GetDesignation")]
        public List<EmployeeModel> GetDesignation()
        {
            EmployeeModel EmployeeModel = new EmployeeModel();
            return EmployeeModel.GetDesignation();
        }


        [HttpPost]
        [Route("AddTasks")]
        public List<TaskModel> AddTasks([FromBody] TaskModel taskModel)
        {
            return taskModel.AddTasks();
        }

        [HttpPost]
        [Route("DeleteTasks")]
        public string DeleteTasks([FromBody] TaskModel taskModel)
        {
            return taskModel.DeleteTasks();
        }

        [HttpPost]
        [Route("GetTaskDetails")]
        public TaskModel GetTaskDetails([FromBody] TaskModel taskModel)
        {
            return taskModel.GetTaskDetails();
        }

        [HttpPost]
        [Route("GetTasksList")]
        public List<TaskModel> GetTasksList([FromBody] TaskModel taskModel)
        {
            return taskModel.GetTasksList();
        }

        [HttpPost]
        [Route("GetTaskSubjects")]
        public TaskModel GetTaskSubjects([FromBody] TaskModel taskModel)
        {
            return taskModel.GetTaskSubjects();
        }


        [HttpPost]
        [Route("GetTaskType")]
        public TaskModel GetTaskType([FromBody] TaskModel taskModel)
        {
            return taskModel.GetTaskType();
        }

        [HttpPost]
        [Route("EditTaskDetails")]
        public TaskModel EditTaskDetails([FromBody] TaskModel taskModel)
        {
            return taskModel.EditTaskDetails();
        }

    }
}