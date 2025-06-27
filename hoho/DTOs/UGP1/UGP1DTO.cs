namespace hoho.DTOs.UGP1
{
    public class UGP1DTO
    {
        public int? Id { get; set; }
        public int AlternateUoMId { get; set; } // Id đơn vị thay thế
        public double AltQty { get; set; }
        public double BaseQty { get; set; }

    }
}
