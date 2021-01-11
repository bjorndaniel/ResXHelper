using System;
using System.Text.Json.Serialization;

namespace ResXHelper.Model
{
    public class Language : IComparable
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("nativeName")]
        public string NativeName { get; set; }
        public int CompareTo(object obj) =>
            ((Language)obj)?.Name.CompareTo(Name) ?? 0;
    }
}
