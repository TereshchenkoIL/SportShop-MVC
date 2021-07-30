using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MySportShop.Models.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public DateTime Creation_Date { get; set; }
       
        public string Id { get; set; }
        [ForeignKey("Id")]
        public AppUser User { get; set; }
        public List<OrderInfo> OrdersInfo { get; set; }

    }
}
