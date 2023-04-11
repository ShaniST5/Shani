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
    public class DeleteCustomerController : ApiController
    {
        igroup195_prod_DB db = new igroup195_prod_DB();

        [HttpDelete]
        [Route("api/CustomerDetails/DeleteCustomer/{id}")]
        public IHttpActionResult DeleteCustomer(string id)
        {
            try
            {
                var customer = db.Customers.FirstOrDefault(c => c.CustomerID == id);//מבצעת שאילתה בטבלת לקוחות כדי למצוא את הלקוח הראשון שהמזהה שלו תואם את הערך של הפרמטר מזהה.

                if (customer == null)
                {
                    return NotFound();//אם לא נמצא לקוח, השיטה מחזירה תגובה
                }

                db.Customers.Remove(customer);
                db.SaveChanges();
                //אם נמצא לקוח מסירה אותו מטבלת לקוחות ושומרת את השינויים במסד הנתונים בשיטת "SaveChanges" של המחלקה "DbContext" .

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting customer: {ex.Message}");//במידה ויש חריגות נשלחת הודעת שגיאה
            }
        }

    }
}
