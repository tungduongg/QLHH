using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace hoho.Data
{
    public class OUOM
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;     // unique
        public string Name { get; set; } = string.Empty;
        public  ICollection<OUGP> BaseUnitGroups { get; set; } = new List<OUGP>();

        public  ICollection<UGP1> AlternateUnits { get; set; } = new List<UGP1>();

    }


    

}
