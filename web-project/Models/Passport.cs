using System.ComponentModel.DataAnnotations.Schema;

namespace web_project.Models
{
    public class Passport
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public DateTime Birthdate { get; set; }
        public bool Gender { get; set; }
        public string Nationality { get; set; }
        public string ImagePath { get; set; }

        public virtual User User { get; set; }
    }
}
