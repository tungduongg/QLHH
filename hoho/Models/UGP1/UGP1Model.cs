namespace hoho.Models.UGP1
{
    public class UGP1Model
    {
        public int Id { get; set; }
        public int FatherId { get; set; }
        public int AlternateUoMId { get; set; } // link ID OUOM  
        public double AltQty { get; set; }
        public double BaseQty { get; set; }
    }
}
