using System.Text.Json.Serialization;

namespace ConsumingPokemonAPI_V2
{
    public class Pokemon
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
