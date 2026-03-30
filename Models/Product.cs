namespace Demo.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }

        [Column("ProductCode")]
        public string ProductCode { get; set; }

        [Column("CompanyID")]
        public long CompanyId { get; set; }

        [Column("CategoryID")]
        public long? CategoryId { get; set; }

        [Column("ProductTypeID")]
        public long ProductTypeId { get; set; }

        [Column("BaseUnitID")]
        public long BaseUnitId { get; set; }

        [Column("DefaultTaxRateID")]
        public long? DefaultTaxRateId { get; set; }

        [Column("IsActive")]
        public bool IsActive { get; set; }

        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        // Fields not present in current DB remain not mapped
        [NotMapped]
        public decimal Price { get; set; }

        [NotMapped]
        public string Image { get; set; }

        [NotMapped]
        public string Category { get; set; } = "Fashion";

        [NotMapped]
        public double Rating { get; set; } = 5.0;

        [NotMapped]
        public int Reviews { get; set; } = 0;

        [NotMapped]
        public string Description { get; set; } = string.Empty;
    }
}