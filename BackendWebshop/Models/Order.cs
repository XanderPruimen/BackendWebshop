using BackendWebshop.DTO_s;

namespace BackendWebshop.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public string Count { get; set; } 
        //public virtual ICollection<Item> Items { get; set; }
    }
}
