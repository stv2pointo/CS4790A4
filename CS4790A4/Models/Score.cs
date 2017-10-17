using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace CS4790A4.Models
{
    [Table("Score")]
    public class Score
    {
        [Key]
        public int Id { get; set; }

        public int? proposalId { get; set; }
        public int? criteriaId { get; set; }

        [Range(0, 35)]
        public int? givenScore { get; set; }

        
        public int? editedBy { get; set; }

        //[DataType(DataType.DateTime)]
        public DateTime editTime { get; set; }



        public static List<Score> GetProposalScores(int? propId)
        {
            ARCCproposalsDbContext db = new ARCCproposalsDbContext();
            List<Score> allScores = new List<Score>();
            List<Score> scorz = new List<Score>();
            //int numScrz = 0;
            allScores = db.scores.ToList();
            foreach(Score s in allScores)
            {
                if(s.proposalId == propId)
                {
                    //numScrz++;
                    scorz.Add(s);
                }
            }
            //Debug.Write("Number of scores in results: " + scorz.Count);
            return scorz;
        }

        public static void UpdateScore(Score score)
        {
            ARCCproposalsDbContext db = new ARCCproposalsDbContext();
            db.Entry(score).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}