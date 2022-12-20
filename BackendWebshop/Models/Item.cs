namespace BackendWebshop.Models
{
    public class Item
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public string ItemInfo { get; set; } = string.Empty; 
        public string ItemImage { get; set; } = string.Empty;
        public decimal ItemPrice { get; set; } 

    }
}
