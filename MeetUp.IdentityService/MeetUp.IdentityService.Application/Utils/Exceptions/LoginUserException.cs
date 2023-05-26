namespace MeetUp.IdentityService.Application.Utils.Exceptions
{
    public class LoginUserException : Exception
    {
        public LoginUserException(string message="Incorrect data!") : base() { }
    }
}
