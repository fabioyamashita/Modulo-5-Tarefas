using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsumingValorantAPI
{
    public class SkinLevels
    {
        [JsonPropertyName("data")]
        public SkinLevelsDetails[] Data { get; set; }

        [JsonPropertyName("status")]
        public long Status { get; set; }
    }
}
