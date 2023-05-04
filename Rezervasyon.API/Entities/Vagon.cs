using System.ComponentModel.DataAnnotations;

namespace Rezervasyon.API.Entities
{
    public class Vagon
    {
        public string TrenAd { get; set; }
        [Key]
        public string Ad { get; set; }
        public int Kapasite { get; set; }
        public int DoluKoltukAdet { get; set; }
    }
}