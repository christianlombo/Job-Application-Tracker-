using System;
using System.ComponentModel.DataAnnotations;

namespace JobApplicationTracker.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; } // Store hashed password

        public DateTime CreatedAt { get; set; }
    }
}
