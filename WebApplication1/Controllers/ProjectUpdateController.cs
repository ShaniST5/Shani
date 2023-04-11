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
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class ProjectController : ApiController
    {
        igroup195_prod_DB db = new igroup195_prod_DB();

        // PUT api/projects
        [HttpPut]
        [Route("api/Project/{projectID}/UpdateProjectName/{projectName}")]
        public HttpResponseMessage UpdateProjectName(int projectID, string projectName)//הפונקציה מקבלת כפרמטרים את מזהה הפרויקט ושם הפרויקט החדש.
        {
            try
            {
                var project = db.Projects.Find(projectID);//מנסה למצוא את הפרויקט במסד הנתונים על ידי המזהה
                if (project == null)//במקרה שהוא לא נמצא, מחזירה שגיאת 404
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Project with id {projectID} not found");
                }

                project.ProjectName = projectName;
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, project);//אם הפרויקט נמצא, הפונקציה מעדכנת את שם הפרויקט לשם החדש ושומרת את השינוי במסד הנתונים
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

    }
}