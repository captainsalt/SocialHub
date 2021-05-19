namespace SocialHub.Application.Interfaces
{
    public interface ICryptographyService
    {
        string Hash(string input);

        bool IsMatch(string input, string hashedString);
    }
}
