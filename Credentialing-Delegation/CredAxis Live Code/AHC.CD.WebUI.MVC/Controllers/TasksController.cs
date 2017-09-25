using AHC.CD.Business.TaskTracker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Controllers
{
    /// <summary>
    /// Date : 25th Sept 2017
    /// Created By : Manideep Innamuri  
    /// Description : Task Tracker Controller (Code Refactoring for Performance)
    /// </summary>
    public class TasksController : Controller
    {
        // GET: Tasks

        public TasksController()
        {
            
        }
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Author: Manideep Innamuri
        /// Description : Method to get all the Tasks Assigned to Logged in User
        /// </summary>
        /// <param name="CDUserID"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetMyDailyTasks(int CDUserID)
        {
            try
            {

            }
            catch(Exception ex)
            {
                throw ex;
            }
            return null;
        }
    }
}