namespace hoho.Models.OITW
{
    public class OITWModel
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string WarehouseCode { get; set; } = string.Empty;
        public decimal QuantityOnHand { get; set; }
    }
}
