using hoho.Models;
using Microsoft.EntityFrameworkCore;

namespace hoho.Data
{
    public class QLHHDatacontext :DbContext
    {
        public QLHHDatacontext(DbContextOptions options) : base(options)
        {
           }       
        public DbSet<OITM> Items { get; set; } = null!;
        public DbSet<OITW> ItemWarehouses { get; set; } = null!;
        public DbSet<OUOM> UnitsOfMeasure { get; set; }= null!;
        public DbSet<OUGP> UnitOfMeasureGroups { get; set; }= null!;
        public DbSet<UGP1> UnitConversions { get; set; } =null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure unique constraints
            modelBuilder.Entity<OITM>()
                .HasIndex(e => e.ItemCode)
                .IsUnique();

            modelBuilder.Entity<OUOM>()
                .HasIndex(e => e.Code)
                .IsUnique();

            modelBuilder.Entity<OUGP>()
                .HasIndex(e => e.Code)
                .IsUnique();

            // Configure relationships
            // OITW → OITM
            modelBuilder.Entity<OITW>()
                       .HasOne(e => e.Item)
                       .WithMany(e => e.WarehouseStocks)
                       .HasForeignKey(e => e.ItemId)
                       .OnDelete(DeleteBehavior.Cascade);   


            //modelBuilder.Entity<Character>()
            //    .HasMany(x => x.Weapons)
            //    .WithMany()
                

            // OITM → OUGP
            modelBuilder.Entity<OITM>()
                        .HasOne(e => e.UnitOfMeasureGroup)
                        .WithMany(e => e.Items)
                        .HasForeignKey(e => e.OUGPId)
                        .OnDelete(DeleteBehavior.Cascade);

            // OUGP → OUOM (BaseUom)
            modelBuilder.Entity<OUGP>()
                        .HasOne(e => e.BaseUnitOfMeasure)
                        .WithMany(e => e.BaseUnitGroups)
                        .HasForeignKey(e => e.BaseUomId)
                        .OnDelete(DeleteBehavior.Cascade);

            // UGP1 → OUGP (FatherId)
            modelBuilder.Entity<UGP1>()
                        .HasOne(e => e.UnitGroup)                    // Mỗi UGP1 chỉ thuộc 1 OUGP                                               
                        .WithMany(e => e.UnitConversions)      // Một OUGP có nhiều UGP1
                        .HasForeignKey(e => e.FatherId)         
                        .OnDelete(DeleteBehavior.Cascade);

            // UGP1 → OUOM (AlternateUoM)
            modelBuilder.Entity<UGP1>()
                        .HasOne(e => e.AlternateUnitOfMeasure)
                        .WithMany(e => e.AlternateUnits)
                        .HasForeignKey(e => e.AlternateUoMId)
                        .OnDelete(DeleteBehavior.Restrict);

            // Configure composite unique constraint for warehouse stock
            modelBuilder.Entity<OITW>()
                        .HasIndex(e => new { e.ItemId, e.WarehouseCode })
                        .IsUnique();

            modelBuilder.Entity<UGP1>()
                .HasIndex(e => new { e.FatherId, e.AlternateUoMId })
                .IsUnique(); //chỉ ngăn trùng trong cùng 1 nhóm
                            //1 id cha chỉ 1 chuyển đổi 

        }

    }
}
