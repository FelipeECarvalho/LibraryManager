namespace LibraryManager.Application.Commands.Auth
{
    public sealed record AuthResponse
    {
        public AuthResponse(string token, string refreshToken)
        {
            Token = token;
            RefreshToken = refreshToken;
        }

        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }
}
