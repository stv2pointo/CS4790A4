using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CS4790A4.ViewModels;
using CS4790A4.Models;

namespace CS4790A4.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(Session["Username"] != null)
            {
                RedirectToAction("Index", "ProposalScores");
            }

            return View();
 
          
        }

    }
}