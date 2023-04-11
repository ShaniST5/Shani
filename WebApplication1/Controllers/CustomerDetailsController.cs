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
    public class CustomerDetailsController : ApiController
    {
        igroup195_prod_DB db = new igroup195_prod_DB();

        [HttpGet]
        [Route("api/CustomerDetails")]
        public IHttpActionResult GetCustomerDetails([FromBody] JObject data)
        {
            try
            {
                string email = data["CustomerEmail"].ToString(); //מחלצת את הערך של המאפיין "CustomerEmail" מאובייקט ה-JSON הקלט ומאחסנת אותו במשתנה מקומי.

                var customer = db.Customers.FirstOrDefault(cust => cust.CustomerEmail == email); //מבצעת שאילתה בטבלת לקוחות במסד הנתונים כדי למצוא את הלקוח הראשון שהמייל שלו תואם את הערך של המשתנה אימייל

                if (customer == null)
                {
                    return NotFound();//אם לא נמצא לקוח, השיטה מחזירה תגובת לא נמצא
                }

                var customerDetails = new CustomerDetailsDTO //אם נמצא לקוח, השיטה יוצרת אובייקט דיטיאו חדש וממלאת את המאפיינים שלו בערכים מאובייקט הלקוח ממסד הנתונים.
                {
                    CustomerName = customer.CustomerName,
                    CustomerID = customer.CustomerID,
                    CustomerPhone = customer.CustomerPhone,
                    CustomerEmail = customer.CustomerEmail,
                    CustomerAdress = customer.CustomerAdress,
                    //CustomerIsPotential = customer.CustomerIsPotential
                };

                return Ok(customerDetails);//השיטה מחזירה תגובה  עם האובייקט החדש שנוצר
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving customer details: {ex.Message}");//אם מתרחשים חריגים כלשהם במהלך הביצוע, מוחזרת הודעת שגיאה
            }
        }
    }


}
