using System.ComponentModel.DataAnnotations.Schema;

namespace web_project.Models
{
    public class DrivingLicence
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Categories { get; set; }
        public string ImagePath { get; set; }

        public virtual User User { get; set; }
    }
}
