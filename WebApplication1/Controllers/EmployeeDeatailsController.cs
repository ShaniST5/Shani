using SignIn;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.DTO;
using Newtonsoft.Json.Linq;

public class EmployeeDetailsController : ApiController
{
    igroup195_prod_DB db = new igroup195_prod_DB();

    [HttpGet]
    [Route("api/EmployeeDetails")]
    public IHttpActionResult GetEmployeeDetails([FromBody] JObject data)
    {
        try
        {
            string email = data["EmployeeEmail"].ToString();

            var employee = db.Employees.FirstOrDefault(emp => emp.EmployeeEmail == email);//מחזיר פרטי עובד על סמך כתובת המייל שלו 

            if (employee == null)
            {
                return NotFound();
            }

            var employeeDetails = new EmployeeDeatailsDTO
            {
                EmployeePK = employee.ID,
                EmployeeName = employee.EmployeeName,
                EmployeeID = employee.EmployeeID,
                EmployeePhone = employee.EmployeePhone,
                EmployeeEmail = employee.EmployeeEmail,
            };

            return Ok(employeeDetails);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error retrieving employee details: {ex.Message}");
        }
    }
}


