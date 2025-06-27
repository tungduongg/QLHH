using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hoho.Data
{
    public class OITW
    {
        [Key]
        public int Id { get; set; }

        public int ItemId { get; set; }
        [Required]

        public string WarehouseCode { get; set; } = string.Empty;
        [Precision(18, 2)]

        public decimal QuantityOnHand { get; set; }

        [ForeignKey("ItemId")]
        public OITM? Item { get; set; }
    }
}
