namespace LibraryManager.Core.ValueObjects
{
    public sealed record Address(
        string Street,
        string Number,
        string District,
        string City,
        string ZipCode,
        string State,
        string CountryCode,
        string Observation)
    {
        public decimal? Latitude { get; private set; }

        public decimal? Longitude { get; private set; }

        public override string ToString()
        {
            var parts = new List<string>
            {
                $"{Street} {Number}"
            };

            if (!string.IsNullOrWhiteSpace(Observation))
            {
                parts.Add(Observation);
            }

            parts.Add(District);
            parts.Add($"{ZipCode} {City} - {State}");
            parts.Add(CountryCode);

            return string.Join(", ", parts);
        }
    }
}
