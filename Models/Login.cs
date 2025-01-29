using System;

namespace JobApplicationTracker.Models
{
    public class Login
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime LoginDate { get; set; }
        public string IpAddress { get; set; }

        public User User { get; set; }  // Foreign key relation
    }
}
