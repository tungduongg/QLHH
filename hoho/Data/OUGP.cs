using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hoho.Data
{
    public class OUGP
    {
        [Key]
        public int Id { get; set; }
        [Required]

        public string Code { get; set; } // unique
        [Required]

        public string Name { get; set; }
        [ForeignKey("BaseUomId")]

        public int BaseUomId { get; set; } // link ID OUOM
        public  OUOM? BaseUnitOfMeasure { get; set; }
        public ICollection<UGP1> UnitConversions { get; set; } = new List<UGP1>();
        public ICollection<OITM> Items { get; set; } = new List<OITM>();

    }

}
