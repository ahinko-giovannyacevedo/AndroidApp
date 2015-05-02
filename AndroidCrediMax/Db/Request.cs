using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ahinko.android.credimax.Db
{
    [Table("Request")]
    public class Request : SQLiteHelper
    {
        [PrimaryKey, AutoIncrement, Column("requestID")]
        public int RequestID { get; set; }
        
        [Column("userName"), MaxLength(50)]
        public string UserName { get; set; }
        
        [Column("userID")]
        public int UserID { get; set; }

        [Column("createdDate")]
        public DateTime CreatedDate { get; set; }

        [Column("identification"), MaxLength(25)]
        public string Identification { get; set; }

        [Column("telephone"), MaxLength(8)]
        public string Telephone { get; set; }

        [Column("customerName"), MaxLength(150)]
        public string CustomerName { get; set; }

        public void Insert() {
            this.Db.Insert(this);
        }

        public void Update() {
            this.Db.Update(this);
        }

        public void Delete() {
            this.Db.Delete(this);
        }
    }

}