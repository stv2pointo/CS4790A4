using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CS4790A4.ViewModels;
using System.Diagnostics;
using CS4790A4.Models;

namespace CS4790A4.Controllers
{
    [AuthorizationFilter]
    public class ProposalScoresController : Controller
    {
        // GET: ProposalScores
        public ActionResult Index()
        {
      
            List<ProposalScores> allProps = new List<ProposalScores>();
            allProps = Repository.GetAllProposals();
            if (allProps.Count > 0)
            {
                return View(allProps);
            }
            return View();
            
        }

        // get for partial view edit
        public ActionResult AjaxEdit(int? id)
        {     
            if (id == null)
            {
                id = 1;
            }
            ProposalScores ps = Repository.GetCompleteProposal(id);

            if (ps.proposal != null)
            {
                return PartialView("_RateProposal",ps);
            }
            return RedirectToAction("Index");
        }

        // POST: Proposals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _RateProposal(ProposalScores ps)
        {

            if (ModelState.IsValid)
            {
                int userId = Repository.GetUserId((string)Session["Username"]);
                foreach (ScoreViewModel svm in ps.scoresViews)
                {
                    svm.score.editedBy = userId;
                    svm.score.editTime = DateTime.Now;
                }
                Repository.UpdateScores(ps);

                return RedirectToAction("Index");
            }
            return View(ps);
        }

    }
}