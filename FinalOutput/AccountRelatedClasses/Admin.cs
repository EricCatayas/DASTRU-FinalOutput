namespace FinalOutput
{
    public class Admin : Account
    {

        public Admin(string username, string password) : base(username, password)
        {
            this.UserType = UserType.Admin;
        }
    }


    





}
