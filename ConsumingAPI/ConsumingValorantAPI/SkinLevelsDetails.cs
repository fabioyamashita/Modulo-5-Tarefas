using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsumingValorantAPI
{
    public class SkinLevelsDetails : IEquatable<SkinLevelsDetails>
    {
        [JsonPropertyName("assetPath")]
        public string AssetPath { get; set; }

        [JsonPropertyName("displayIcon")]
        public string DisplayIcon { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("levelItem")]
        public string LevelItem { get; set; }

        [JsonPropertyName("streamedVideo")]
        public string StreamedVideo { get; set; }

        [JsonPropertyName("uuid")]
        public Guid Uuid { get; set; }


        public bool Equals(SkinLevelsDetails? other)
        {
            return null != other && DisplayName == other.DisplayName;
        }

        public override bool Equals(object? obj)
        {
            if (!(obj is SkinLevelsDetails))
            {
                return false;
            }

            SkinLevelsDetails other = obj as SkinLevelsDetails;
            return DisplayName.Equals(other.DisplayName);
            //return base.Equals(obj);
        }


        public override int GetHashCode()
        {
            return DisplayName.GetHashCode();
            //return base.GetHashCode();
        }

    }
}
