using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SQLite;

namespace ahinko.android.credimax.Db
{
    [Table("RequestDetail")]
    public class RequestDetail
    {
        [PrimaryKey, AutoIncrement, Column("requestDetailID")]
        public int RequestDetailID { get; set; }

        [Indexed, Column("requestID")]
        public int RequestID { get; set; }

        [Column("itemNumber")]
        public string ItemNumber { get; set; }
        
        [Column("barcode")]
        public string BarCode { get; set; }
        
        [Column("name")]
        public string Name { get;set;}
        
        [Column("description")]
        public string Description { get; set; }
        
        [Column("quantity")]
        public decimal Quantity { get; set; }
        
        [Column("price")]
        public decimal Price { get; set; }
        
        [Column("planID")]
        public int PlanID { get; set; }
        
        [Column("planName")]
        public string PlanName { get; set; }
    }
}