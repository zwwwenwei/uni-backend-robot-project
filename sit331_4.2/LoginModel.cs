namespace sit331_4._2
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginModel(string email, string password) 
        {
            Email = email;
            Password = password;
        }
    }
}
