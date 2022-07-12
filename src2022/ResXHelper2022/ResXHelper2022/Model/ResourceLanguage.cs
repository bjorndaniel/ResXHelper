namespace ResXHelper2022.Model
{
    public class ResourceLanguage : IComparable
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("nativeName")]
        public string NativeName { get; set; }
        public int CompareTo(object obj) =>
            ((ResourceLanguage)obj)?.Name.CompareTo(Name) ?? 0;
    }
}
