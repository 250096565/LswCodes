using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AliPayMvc.Models
{
    public class PayModel
    {
        public string WIDout_trade_no { get; set; }
        public string WIDsubject { get; set; }
        public string WIDtotal_fee { get; set; }
        public string WIDbody { get; set; }
    }
}