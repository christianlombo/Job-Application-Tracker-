using System.ComponentModel.DataAnnotations;

namespace JobApplicationTracker.Models
{
    public class JobApplication
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Company name is required")]
        [Display(Name = "Company Name")]
        [StringLength(100)]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Job title is required")]
        [Display(Name = "Job Title")]
        [StringLength(100)]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Application date is required")]
        [Display(Name = "Application Date")]
        [DataType(DataType.Date)]
        public DateTime ApplicationDate { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }

        [Display(Name = "Job Link")]
        [Url(ErrorMessage = "Please enter a valid URL")]
        public string? JobLink { get; set; }

        [Display(Name = "Notes")]
        [DataType(DataType.MultilineText)]
        public string? Notes { get; set; }

        public string? UserId { get; set; }

        [Display(Name = "Created At")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
