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


namespace WebApplication1.Controllers
{
    public class ListCustomrController : ApiController
    {
        igroup195_prod_DB db = new igroup195_prod_DB();

        [HttpGet]
        [Route("api/CustomerDetails/GetAllCustomers")]
        public IHttpActionResult GetAllCustomers()
        {
            try
            {
                var customers = db.Customers.ToList();//שואבת את כל פרטי הלקוח ממסד הנתונים ומשייכת אותו לרשימה

                if (customers == null || customers.Count == 0)//בודק אם אין לקוחות ברשימה
                {
                    return NotFound();//מחזירה תגובה אם זה המקרה
                }

                var customerDetailsList = new List<CustomerDetailsDTO>();//עבור כל לקוח ברשימה, נוצר מופע חדש

                foreach (var customer in customers)
                {
                    var customerDetails = new CustomerDetailsDTO //האובייקט CustomerDetailsDTO נוסף לרשימה של customerDetailsList
                    {
                        CustomerName = customer.CustomerName,
                        CustomerID = customer.CustomerID,
                        CustomerPhone = customer.CustomerPhone,
                        CustomerEmail = customer.CustomerEmail,
                        CustomerAdress = customer.CustomerAdress,
                        CustomerIsPotential = customer.isPotential
                    };

                    customerDetailsList.Add(customerDetails);
                }

                return Ok(customerDetailsList);//רשימת פרטי הלקוח מוחזרת
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving customer list: {ex.Message}");//טיפול בחריגות
            }
        }

    }
}
