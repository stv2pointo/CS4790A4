using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CS4790A4.Models
{
    [Table("Criteria")]
    public class Criteria
    {
        [Key]
        public int Id { get; set; }


        //[Required, StringLength(30), DisplayName("Proposal")]
        public string CriteriaName { get; set; }


        //[Required, StringLength(300), DisplayName("Description")]
        public string description { get; set; }


        //[Required, StringLength(400), DisplayName("Ratings' Suggestions")]
        public string ratingsSugg { get; set; }


        //[Required, DisplayName("Max Score"), Range(0, 35)]
        public int maxScore { get; set; }

        public static Criteria GetCriteria(int? id)
        {
            ARCCproposalsDbContext db = new ARCCproposalsDbContext();
            var crit = db.criterias.Find(id);
            //crit.ratingsSugg = crit.ratingsSugg.Replace("~", "\n");
            return crit;
        }

        public static void Dispose()
        {
            ARCCproposalsDbContext db = new ARCCproposalsDbContext();
            db.Dispose();
        }


    }
}