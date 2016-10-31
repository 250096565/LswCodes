namespace Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Photo")]
    public partial class Photo
    {
        public DateTime date { get; set; }

        public int id { get; set; }

        [StringLength(50)]
        public string cloudid { get; set; }

        [StringLength(50)]
        public string roleid { get; set; }

        [StringLength(800)]
        public string url { get; set; }

        [StringLength(50)]
        public string type { get; set; }

        public bool? isCover { get; set; }

        public bool? isCheck { get; set; }

        [StringLength(50)]
        public string subtype { get; set; }
    }
}
