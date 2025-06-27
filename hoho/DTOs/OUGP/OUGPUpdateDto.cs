using hoho.DTOs.UGP1;

namespace hoho.DTO
{
    public class OUGPUpdateDto
    {
        public int id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int BaseUomId { get; set; }
        public List<UGP1DTO> UGP1 { get; set; } //Không có Id
    }

}
