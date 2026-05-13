namespace ChapeauProject.Models
{
    public class LoginModel
    {
        public int StaffID { get; set; }
        public string Password { get; set; }

        public LoginModel()
        {

        }

        public LoginModel(int staffID, string password)
        {
            StaffID = staffID;
            Password = password;
        }
    }
}
