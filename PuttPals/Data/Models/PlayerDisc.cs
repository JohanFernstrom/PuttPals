using System.Text.Json.Serialization;

namespace PuttPals.Data.Models
{
    public class PlayerDisc
    {
        public int PlayerId { get; set; }
        [JsonIgnore]
        public Player Player { get; set; }

        public int DiscId { get; set; }
        [JsonIgnore]
        public Disc Disc { get; set; }

        public int Count { get; set; } = 1; // Default count to 1
    }
}
