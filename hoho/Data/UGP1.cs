using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hoho.Data
{
    public class UGP1
    {
        [Key]

        public int Id { get; set; }
        public int FatherId { get; set; }
        public int AlternateUoMId { get; set; } // link ID OUOM
        public double AltQty { get; set; }
        public double BaseQty { get; set; }

        [ForeignKey("FatherId")]
        public  OUGP? UnitGroup { get; set; }
        [ForeignKey("AlternateUoMId")]
        
        public  OUOM? AlternateUnitOfMeasure { get; set; }
    }
}
