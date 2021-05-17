using LanguageExt.Common;

namespace SocialHub.Application.Models
{
    public static class Errors
    {
        public static readonly Error InvalidLogin = Error.New("Invalid login credentials");

        public static readonly Error UsernameInUse = Error.New("Username already in use");

        public static readonly Error EmailInUse = Error.New("Email already in use");
    }
}
