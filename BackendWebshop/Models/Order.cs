using BackendWebshop.DTO_s;

namespace BackendWebshop.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public string Count { get; set; } = string.Empty;
        //public virtual ICollection<Item> Items { get; set; }
    }
}
