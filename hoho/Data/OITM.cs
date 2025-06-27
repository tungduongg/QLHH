using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace hoho.Data
{
    public class OITM
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ItemCode { get; set; } = string.Empty;

        [Required]
        public string ItemName { get; set; } = string.Empty;
        public string ItemGroup { get; set; } = string.Empty;
        public int? OUGPId { get; set; }
        [Precision(18, 2)]

        public decimal Price { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<OITW> WarehouseStocks { get; set; } = new List<OITW>();
        public OUGP? UnitOfMeasureGroup { get; set; }



    }
}
