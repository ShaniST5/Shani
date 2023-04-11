using System.Web.Http;
using WebApplication1.DTO;
using SignIn;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace WebApplication1.Controllers
{
    public class CustomerUpdateController : ApiController
    {
        igroup195_prod_DB db = new igroup195_prod_DB();

        [HttpPut]
        [Route("api/CustomerUpdate/{id}")]
        public HttpResponseMessage Put(string id, [FromBody] CustomerDetailsDTO updatedCustomer)
        {
            try
            {
                // חיפוש הלקוח במסד הנתונים לפי ה-ID
                var customer = db.Customers.FirstOrDefault(c => c.CustomerID == id);

                // בדיקה אם הלקוח נמצא במסד הנתונים
                if (customer == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, $"Customer with ID {id} not found");
                }

                // עדכון הפרטים של הלקוח לפי הנתונים החדשים
                customer.CustomerEmail = updatedCustomer.CustomerEmail;
                customer.CustomerName = updatedCustomer.CustomerName;
                customer.CustomerPhone = updatedCustomer.CustomerPhone;
                customer.CustomerAdress = updatedCustomer.CustomerAdress;
                //customer.CustomerIsPotential = updatedCustomer.CustomerIsPotential; //שגיאה בטיפוס כנראה

                // שמירת השינויים במסד הנתונים
                db.SaveChanges();

                // מחזיר תשובה במידה והשינויים נשמרו בהצלחה
                return Request.CreateResponse(HttpStatusCode.OK, customer);
            }
            catch (Exception e)
            {
                // מחזיר תשובת שגיאה במידה ואירעה שגיאה בזמן עדכון המידע
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

    }
}
