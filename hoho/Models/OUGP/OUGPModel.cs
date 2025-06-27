namespace hoho.Models.OUGP
{
    public class OUGPModel
    {
        public int Id { get; set; }
        public string Code { get; set; } // unique
        public string Name { get; set; }
        public int BaseUom { get; set; } // link ID OUOM


    }
}
