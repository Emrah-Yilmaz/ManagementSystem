namespace ManagementSystem.Domain.Utilities
{
    public static class Shared
    {
        public struct JwtClaims
        {
            public const string UserId = "UserId";
            public const string FirstName = "Name";
            public const string LastName = "LastName";
            public const string UserName = "UserName";
            public const string Email = "Email";
            public const string Unknown = "Unknown";
        }
    }
}
