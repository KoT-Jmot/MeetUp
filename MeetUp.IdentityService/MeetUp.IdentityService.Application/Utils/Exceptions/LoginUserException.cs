namespace MeetUp.IdentityService.Application.Utils.Exceptions
{
    public class LoginUserException : Exception
    {
        public LoginUserException(string message="Incorrect login or password!") : base(message) { }
    }
}
