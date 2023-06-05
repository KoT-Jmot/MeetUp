namespace MeetUp.IdentityService.Application.Utils.Exceptions
{
    public class RegistrationUserException : Exception
    {
        public RegistrationUserException(string message="incorrect user data!") : base(message) { }
    }
}
