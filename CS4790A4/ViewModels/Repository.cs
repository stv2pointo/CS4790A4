using System;
using System.Collections.Generic;
using System.Linq;
using CS4790A4.Models;


namespace CS4790A4.ViewModels
{
    public class Repository
    {
        // get one proposal based on id
        public static Proposal GetProposal(int? id)
        {
            Proposal proposal = Proposal.GetProposal(id);
            return proposal;
        }

        // make a list of scores for a certain proposal including the criteria for each score
        public static List<ScoreViewModel> GetScoresViews(int? propId) 
        {
            List<ScoreViewModel> scoreViewModels = new List<ScoreViewModel>();

            List<Score> scores = new List<Score>();
            scores = Score.GetProposalScores(propId);
            List<Criteria> criterias = new List<Criteria>();
            foreach (Score s in scores)
            {
                ScoreViewModel svm = new ScoreViewModel();
                svm.score = s;
                svm.criteria = Criteria.GetCriteria(s.criteriaId);
                scoreViewModels.Add(svm);
            }
            return scoreViewModels;
        }

        // get a proposal with all of its scores and stuff
        public static ProposalScores GetCompleteProposal(int? id)
        {
            ProposalScores ps = new ProposalScores();
            ps.proposal = GetProposal(id);
            ps.scoresViews = GetScoresViews(id);
            return ps;
        }

        // get all proposals wth all of their goodies
        public static List<ProposalScores> GetAllProposals()
        {
            List<ProposalScores> allProps = new List<ProposalScores>();
            List<int> propIds = Proposal.GetProposalIds();
            foreach (int pId in propIds)
            {
                allProps.Add(GetCompleteProposal(pId));
            }
            if(allProps.Count > 0)
            {
                //List<ProposalScores> noScoreList = allProps.OrderBy(p => p.proposal.isComplete).ToList();
                List<ProposalScores> noScoreList = allProps.Where(p => p.proposal.isComplete == false).ToList();
                List<ProposalScores> scoredList = allProps.Where(p => p.proposal.isComplete == true).ToList();
                List<ProposalScores> sortedList = RankBySummedScores(scoredList);
                foreach(ProposalScores ps in noScoreList)
                {
                    sortedList.Add(ps);
                }
                return sortedList;
            }

            return allProps;
        }

        public class RankedProposal
        {
            public ProposalScores ps { get; set; }
            public int summedScores { get; set; }
        }
        public static List<ProposalScores> RankBySummedScores(List<ProposalScores> unrankedList)
        {
            List<RankedProposal> rankableList = new List<RankedProposal>();
            foreach (ProposalScores ps in unrankedList)
            {
                int sumScores = 0;
                foreach(ScoreViewModel svm in ps.scoresViews)
                {
                    if(svm.score.givenScore != null)
                    {
                        sumScores += (int)svm.score.givenScore;
                    }
                }
                RankedProposal rp = new RankedProposal();
                rp.ps = ps;
                rp.summedScores = sumScores;
                rankableList.Add(rp);
            }
            var rankedBySum = rankableList.OrderByDescending(s => s.summedScores).ToList();
            List<ProposalScores> rankedList = new List<ProposalScores>();
            foreach(RankedProposal rp in rankedBySum)
            {
                rankedList.Add(rp.ps);
            }
            return rankedList;
        }

        // update a set of scores
        public static void UpdateScores(ProposalScores ps) {   //List<ScoreViewModel> svms) {

            Proposal proposal = new Proposal();
            proposal = ps.proposal;
            List<ScoreViewModel> svms = new List<ScoreViewModel>();
            svms = ps.scoresViews;
            List<Score> scores = new List<Score>();
            bool completed = true;
   
            foreach (ScoreViewModel svm in svms)
            {
                if(svm.score.givenScore == null)
                {
                    completed = false;
                }
                Score.UpdateScore(svm.score);
            }

            proposal.isComplete = completed;
            Proposal.UpdateProposal(proposal);
      
        }

        public static int GetUserId(String email)
        {
            return User.GetUserId(email);
            
        }

        public static bool CreateUser(User user)
        {
            return User.CreateUser(user);
        }

    }


}