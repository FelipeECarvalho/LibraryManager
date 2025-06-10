namespace LibraryManager.Infrastructure
{
    public class JwtInfoOptions
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int Expires { get; set; }
    }
}
