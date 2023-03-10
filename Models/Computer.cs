// Add folder name "Models" to namespace.
// "Computer" Model is decoupled from Program.cs and Main method.
using System.Text.Json.Serialization;

namespace ModelsTutorial.Models
{
    public class Computer
    {
        [JsonPropertyName("computer_id")]
        public int ComputerId { get; set; }
        // private string _motherboard; private string Motherboard;
        // below acts as a property to the _motherboard field above
        // private string Motherboard { get { return _motherboard; } set { _motherboard = value; } }
        // Below has same functionality as the two above lines
        [JsonPropertyName("motherboard")]
        public string Motherboard { get; set; } = "";

        [JsonPropertyName("cpu_cores")]
        public int? CPUCores { get; set; } = 0;

        [JsonPropertyName("has_wifi")]
        public bool HasWifi { get; set; }

        [JsonPropertyName("has_lte")]
        public bool HasLTE { get; set; }

        [JsonPropertyName("release_date")]
        public DateTime? ReleaseDate { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        // To fix non non-nullable warning, use = "";
        [JsonPropertyName("video_card")]
        public string VideoCard { get; set; } = "";

        // Fix non-nullable string warning, see easier way above, this isn't necessary
        // public Computer()
        // {
        //     if (VideoCard == null)
        //     {
        //         VideoCard = "";
        //     }
        //     if (Motherboard == null)
        //     {
        //         Motherboard = "";
        //     }
        // }
    }
}
