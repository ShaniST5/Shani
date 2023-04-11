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
    public class ProjectsController : ApiController
    {
        //The GetProject operation that retrieves a specific project from the database and maps it to a ProjectsDTO object before returning it.
        igroup195_prod_DB db = new igroup195_prod_DB();

        // GET api/projects/5
        [HttpGet]
        [Route("api/Project/{id}")]
        public ProjectsDTO GetProject(int id) //This method takes an integer parameter ID representing the project ID and returns a ProjectsDTO object containing the project details.
        {
            var project = db.Projects
                .Where(p => p.ProjectID == id)//שיביא לי פרויקט ספציפי
                .Select(p => new ProjectsDTO //This query selects the project from the projects table in the database where the ProjectID is equal to the id parameter.
                {
                    ProjectID = p.ProjectID,
                    ProjectName = p.ProjectName,
                    CustomerPK = p.CustomerPK,
                    Description = p.Description,
                    InsertDate = p.InsertDate,
                    //Deadline = p.Deadline,
                    //בעיה עם טיפוס המשתנה לדעתי
                    isDone = p.isDone
                })
                .FirstOrDefault();//מחזירה את הפרוייקט הראשון שתואם את התנאי

            return project;
        }
    }

}
