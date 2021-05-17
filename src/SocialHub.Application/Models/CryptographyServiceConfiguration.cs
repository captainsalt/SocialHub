namespace SocialHub.Application.Models
{
    public class CryptographyServiceConfiguration
    {
        public CryptographyServiceConfiguration(int iterations)
        {
            Iterations = iterations;
        }

        public int Iterations { get; set; }
    }
}