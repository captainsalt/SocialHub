namespace SocialHub.Application.Services
{
    public interface ICryptographyService
    {
        string Hash(string input);

        bool IsMatch(string input, string hashedString);
    }
}
