// Add folder name "Models" to namespace.
// "Computer" Model is decoupled from Program.cs and Main method.
namespace ModelsTutorial.Models
{
    public class Computer
    {
        public int ComputerId { get; set; }
        // private string _motherboard; private string Motherboard;
        // below acts as a property to the _motherboard field above
        // private string Motherboard { get { return _motherboard; } set { _motherboard = value; } }
        // Below has same functionality as the two above lines
        public string Motherboard { get; set; } = "";

        public int? CPUCores { get; set; } = 0;

        public bool HasWifi { get; set; }

        public bool HasLTE { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public decimal Price { get; set; }
        // To fix non non-nullable warning, use = "";
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
