using System.Text.Json.Serialization;

namespace LINQ_I_Revised
{
    public class Selic
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("selic")]
        public double SelicValue { get; set; }
    }
}
