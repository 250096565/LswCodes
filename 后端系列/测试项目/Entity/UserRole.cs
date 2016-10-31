namespace Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserRole")]
    public partial class UserRole
    {
        [Key]
        [StringLength(50)]
        public string RoleID { get; set; }

        [StringLength(50)]
        public string CloudID { get; set; }

        [StringLength(200)]
        public string name { get; set; }

        public int levelcode { get; set; }

        public DateTime? regdate { get; set; }

        public int areaid { get; set; }

        [StringLength(50)]
        public string shopname { get; set; }

        [StringLength(50)]
        public string mobile { get; set; }

        [StringLength(500)]
        public string addr { get; set; }

        [StringLength(50)]
        public string postcode { get; set; }

        [StringLength(50)]
        public string promoter { get; set; }

        [StringLength(50)]
        public string bindingId { get; set; }

        [StringLength(50)]
        public string bindingId2 { get; set; }

        [StringLength(50)]
        public string bank { get; set; }

        [StringLength(50)]
        public string branch { get; set; }

        [StringLength(50)]
        public string cardnumber { get; set; }

        [StringLength(50)]
        public string cardname { get; set; }

        [StringLength(50)]
        public string idcard { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? fee { get; set; }

        [StringLength(50)]
        public string regionManagerID { get; set; }

        [StringLength(50)]
        public string ZJID { get; set; }

        public bool? status { get; set; }

        [Column(TypeName = "text")]
        public string enterpriseInfo { get; set; }

        [Column(TypeName = "text")]
        public string memo { get; set; }

        [StringLength(50)]
        public string authChar { get; set; }

        [StringLength(50)]
        public string productID { get; set; }

        [StringLength(50)]
        public string orderid { get; set; }

        public bool isOnlineShop { get; set; }

        public bool OnLineCheck { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Column(TypeName = "text")]
        public string description { get; set; }

        public bool cabinet { get; set; }

        [StringLength(50)]
        public string vipcard { get; set; }

        [StringLength(50)]
        public string qq { get; set; }

        [StringLength(50)]
        public string weixin { get; set; }

        [StringLength(150)]
        public string weibo { get; set; }

        [StringLength(150)]
        public string email { get; set; }

        public bool closed { get; set; }

        [StringLength(50)]
        public string nickname { get; set; }

        [StringLength(500)]
        public string transferStat { get; set; }

        public DateTime? payDate { get; set; }

        public DateTime? ExpiredDate { get; set; }

        public string keystring { get; set; }

        public string keystring1 { get; set; }

        public string keystring1Info { get; set; }

        public bool CS_FLAG { get; set; }

        [StringLength(50)]
        public string tracenumber { get; set; }

        public int? cardnumberStatus { get; set; }

        public int? plusid { get; set; }

        public DateTime? plusDate { get; set; }

        [StringLength(50)]
        public string FixedTelephone { get; set; }

        public int? IdType { get; set; }
    }
}
