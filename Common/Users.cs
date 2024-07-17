namespace Common
{
    public class Users
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int CoursesNum { get; set; }
        public bool IsConfirmed { get; set; }
        public string LastView { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool? Status { get; set; } // השתמש ב- bool? עבור שדות שמקבלים null
    }
}