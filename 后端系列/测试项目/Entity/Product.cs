namespace Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [StringLength(50)]
        public string ProductID { get; set; }

        [Required]
        [StringLength(50)]
        public string ClassID { get; set; }

        [StringLength(50)]
        public string Brand { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }

        [StringLength(50)]
        public string ShortName { get; set; }

        [Required]
        [StringLength(50)]
        public string Spec { get; set; }

        [StringLength(50)]
        public string UNIT { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? UnitPrice { get; set; }

        [Column(TypeName = "numeric")]
        public decimal freight { get; set; }

        [Required]
        [StringLength(50)]
        public string Supplier { get; set; }

        [StringLength(8000)]
        public string PMID { get; set; }

        public bool Sales { get; set; }

        [Required]
        [StringLength(8000)]
        public string levels { get; set; }

        public bool ishot { get; set; }

        public bool isbest { get; set; }

        public bool isnew { get; set; }

        public bool ispromotion { get; set; }

        [StringLength(8000)]
        public string describe { get; set; }

        [StringLength(8000)]
        public string brief { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public bool? isBursting { get; set; }

        public bool? isInvestPrj { get; set; }

        public bool? isNormalProduct { get; set; }

        public DateTime date { get; set; }

        public string memo { get; set; }

        [Column(TypeName = "text")]
        public string ProductAttributes { get; set; }

        [Column(TypeName = "text")]
        public string ShortDescribes { get; set; }

        [StringLength(50)]
        public string SendDate { get; set; }

        public int? DPState { get; set; }

        [StringLength(8000)]
        public string AppDescribe { get; set; }

        public int? sortid { get; set; }

        public int? PurchaseStrategy { get; set; }

        public int? PurchaseQuantity { get; set; }

        public int? plusid { get; set; }

        [StringLength(500)]
        public string VideoUrl { get; set; }

        [Column(TypeName = "text")]
        public string ZMDescribes { get; set; }

        [Column(TypeName = "text")]
        public string ZMShortDescribes { get; set; }

        public int? GroupId { get; set; }
    }
}
