using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CS4790A4.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }


        [Required, StringLength(30), EmailAddress, DisplayName("Email Address")]
        public string email { get; set; }


        [Required, StringLength(25), DisplayName("Password")]
        public string password { get; set; }

        public static int GetUserId(string email)
        {
            ARCCproposalsDbContext db = new ARCCproposalsDbContext();
            List<User> users = new List<User>();
            users = db.Users.ToList();
            foreach (User user in users)
            {
                if (user.email == email)
                {
                    return user.Id;
                }
            }
            return 0;
        }

        public static bool CreateUser(User user)
        {
            ARCCproposalsDbContext db = new ARCCproposalsDbContext();
            List<User> users = new List<User>();
            users = db.Users.ToList();
            bool userExists = false;
            foreach (User u in users)
            {
                if (user.email == u.email)
                {
                    userExists = true;
                }
            }
            if (!userExists)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}