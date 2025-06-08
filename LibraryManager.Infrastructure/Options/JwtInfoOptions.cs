namespace LibraryManager.Infrastructure.Options
{
    public class JwtInfoOptions
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int Expires { get; set; }
    }
}
