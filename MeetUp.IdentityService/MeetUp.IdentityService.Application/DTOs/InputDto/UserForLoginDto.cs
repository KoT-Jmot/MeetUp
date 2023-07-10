namespace MeetUp.IdentityService.Application.DTOs.InputDto
{
    public class UserForLoginDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
