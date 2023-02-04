// Add folder name "Models" to namespace.
// "Computer" Model is decoupled from Program.cs and Main method.
namespace ModelsTutorial.Models
{
    public class ComputerSnake
    {
        public int computer_id { get; set; }
        // private string _motherboard; private string Motherboard;
        // below acts as a property to the _motherboard field above
        // private string Motherboard { get { return _motherboard; } set { _motherboard = value; } }
        // Below has same functionality as the two above lines
        public string motherboard { get; set; } = "";

        public int? cpu_cores { get; set; } = 0;

        public bool has_wifi { get; set; }

        public bool has_lte { get; set; }

        public DateTime? release_date { get; set; }

        public decimal price { get; set; }
        // To fix non non-nullable warning, use = "";
        public string video_card { get; set; } = "";

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
