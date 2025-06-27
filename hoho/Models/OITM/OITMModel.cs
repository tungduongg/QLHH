namespace hoho.Models.OITM
{
    public class OITMModel
    {
        public int Id { get; set; }
        public string ItemCode { get; set; } = string.Empty;
        public string ItemName { get; set; } = string.Empty;
        public string ItemGroup { get; set; } = string.Empty;
        public int OUGPId { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; } = true;


    }
}
