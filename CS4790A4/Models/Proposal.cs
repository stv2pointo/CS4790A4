using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace CS4790A4.Models
{
    [Table("Proposal")]
    public class Proposal
    {
        [Key]
        public int Id { get; set; }

        //[Required, StringLength(50), DisplayName("Proposal")]
        public string proposalName { get; set; }

        //[Required, DisplayName("Scored")]
        public bool isComplete { get; set; }

        //[StringLength(25)]
        public string imagePath { get; set; }


        public static Proposal GetProposal(int? id)
        {
            ARCCproposalsDbContext db = new ARCCproposalsDbContext();
            Proposal proposal = new Proposal();
            proposal = db.proposals.Find(id);
            return proposal;

        }

        // return a list of all existing ids for populating main list
        public static List<int> GetProposalIds()
        {
            ARCCproposalsDbContext db = new ARCCproposalsDbContext();
            var props = db.proposals.ToList();
            List<int> propIds = new List<int>();
            if (props == null)
            {
                Debug.Write("proposals is empty");
            }
            else
            {
                foreach (Proposal p in props)
                {
                    propIds.Add(p.Id);
                }
            }

            return propIds;
        }

        // update if scoring is complete
        public static void UpdateProposal(Proposal proposal)
        {
            ARCCproposalsDbContext db = new ARCCproposalsDbContext();
            db.Entry(proposal).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}