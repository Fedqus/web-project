namespace web_project.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PermissionLevel { get; set; }

        public virtual ICollection<Passport> Passports { get; set; }
        public virtual ICollection<DrivingLicence> DrivingLicences { get; set; }
        public virtual ICollection<StudentCard> StudentCards { get; set; }

        public string FullName()
        {
            return $"{Name} {LastName} {MiddleName}";
        }
    }
}
