using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CS4790A4.Models;

namespace CS4790A4.ViewModels
{
    public class ScoreViewModel
    {
        public Score score { get; set; }
        public Criteria criteria { get; set; }
    }
}