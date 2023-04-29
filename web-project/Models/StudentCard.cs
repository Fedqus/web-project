using System.ComponentModel.DataAnnotations.Schema;

namespace web_project.Models
{
    public class StudentCard
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public string EduForm { get; set; }
        public string EduInstitution { get; set; }
        public string ImagePath { get; set; }

        public virtual User User { get; set; }
    }
}
