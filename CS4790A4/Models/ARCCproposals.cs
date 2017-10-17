using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CS4790A4.Models
{
    public class ARCCproposals
    {
    }

    public class ARCCproposalsDbContext : DbContext
    {
        public DbSet<Criteria> criterias { get; set; }
        public DbSet<Proposal> proposals { get; set; }
        public DbSet<Score> scores { get; set; }

        public System.Data.Entity.DbSet<CS4790A4.Models.User> Users { get; set; }
    }
}