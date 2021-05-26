using LanguageExt.Common;

namespace SocialHub.Application.Models
{
    public static class Errors
    {
        public static readonly Error InvalidLogin = Error.New("Invalid login credentials");

        public static readonly Error UsernameInUse = Error.New("Username already in use");

        public static readonly Error EmailInUse = Error.New("Email already in use");

        public static readonly Error UserDoesNotExist = Error.New("User does not exist");

        public static readonly Error PostNotFound = Error.New("Post not found");

        public static readonly Error CannotFollowSelf = Error.New("Cannot follow yourself");

        public static readonly Error AlreadyFollowing = Error.New("You are already following this user");

        public static readonly Error InvalidToken = Error.New("Invalid token");
    }
}
