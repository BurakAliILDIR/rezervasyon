using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Rezervasyon.API.Entities
{
    public class Tren
    {
        [Key] 
        public string Ad { get; set; }
        public ICollection<Vagon> Vagonlar { get; set; }
    }
}