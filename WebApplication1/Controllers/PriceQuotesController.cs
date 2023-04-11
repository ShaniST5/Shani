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
    //הקוד מכיל שתי פעולות, אחת ליצירת הצעת מחיר חדשה ואחת לאחזור הצעת מחיר לפי המזהה שלו.
    public class PriceQuotesController : ApiController
    {
        // הכנסת הקשר לבסיס הנתונים
        igroup195_prod_DB db = new igroup195_prod_DB();

        [HttpPost]
        [Route("api/PriceQuote")]
        public IHttpActionResult CreatePriceQuote([FromBody] PriceDTO priceDTO)
        {
            // יצירת ציטוט מחיר חדש מה-DTO המתקבל
            var newPriceQuote = new PriceQuotes //Inside the action, a new PriceQuotes object is created based on the PriceDTO object sent in the request.
            {
                CustomerPK = priceDTO.Customer_PK,
                ProjectID = priceDTO.Project_Id,
                TotalWorkHours = priceDTO.TotalWorke_Hours,
                DiscoutPercent = priceDTO.Discout_Percent
                //האם צריכה להוסיף גם את שני השדות האחרונים ששמתי בDTO ?
            };

            // עדכון המסד נתונים 
            db.PriceQuotes.Add(newPriceQuote);//האובייקט החדש מתווסף למסד הנתונים 
            db.SaveChanges();//האובייקט החדש נשמר במסד הנתונים

            // החזרת תשובה בהתאם לצלילות ההוספה למסד הנתונים
            return CreatedAtRoute(
                "GetPriceQuoteById",
                new { id = newPriceQuote.CustomerPK},
                newPriceQuote
            );
        }

        [HttpGet]
        [Route("api/pricequotes/{id}", Name = "GetPriceQuoteById")]
        public IHttpActionResult GetPriceQuoteById(int id)//לוקחת מזהה פרמטר שלם שהוא המזהה של הצעת המחיר שיש לאחזר ממסד הנתונים.
        {
            var priceQuote = db.PriceQuotes.Find(id);//הפעולה מאחזרת את הצעת המחיר ממסד הנתונים

            if (priceQuote == null)
            {
                return NotFound();//במידה ולא נמצאה
            }

            return Ok(priceQuote);
            //הפעולה מקבלת מזהה של ציטוט מחיר כפרמטר ומחזירה את הציטוט המתאים ממסד הנתונים
        }
    }
}
