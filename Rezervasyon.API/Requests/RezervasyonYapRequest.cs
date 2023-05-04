using Rezervasyon.API.Entities;

namespace Rezervasyon.API.Requests
{
    public class RezervasyonYapRequest
    {
        public TrenRequest Tren { get; set; }
        public int RezervasyonYapilacakKisiSayisi { get; set; }
        public bool KisilerFarkliVagonlaraYerlestirilebilir { get; set; }
    }

    public class TrenRequest
    {
        public string Ad { get; set; }
       // public List<VagonRequest> Vagonlar { get; set; }
    }

    //public class VagonRequest
    //{
    //    public string Ad { get; set; }
    //    public int Kapasite { get; set; }
    //    public int DoluKoltukAdet { get; set; }
    //}
}
