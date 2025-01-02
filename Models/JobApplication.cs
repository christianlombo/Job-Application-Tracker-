using System.ComponentModel.DataAnnotations;

namespace JobApplicationTracker.Models
{
    public class JobApplication
    {
        public int Id { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string JobTitle { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ApplicationDate { get; set; }

        [Required]
        public string Status { get; set; }

        [Url]
        public string JobLink {  get; set; }
        public string Notes { get; set; }
        public string UserId {  get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
