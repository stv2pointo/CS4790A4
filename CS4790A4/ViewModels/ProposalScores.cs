using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CS4790A4.Models;

namespace CS4790A4.ViewModels
{
    public class ProposalScores
    {
        public Proposal proposal { get; set; }

        public List<ScoreViewModel> scoresViews { get; set; }


        public static ProposalScores GetProposalScores(int id)
        {
            ProposalScores proposalScores = new ProposalScores();

            proposalScores.proposal = Repository.GetProposal(id);

            proposalScores.scoresViews = Repository.GetScoresViews(id);
            return proposalScores;
        }
    }
}