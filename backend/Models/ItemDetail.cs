namespace UEdit.Models
{
    public class ItemDetail
    {
        public int Id { get; set; }
        public string MenuKey { get; set; } = "";
        public int SubId { get; set; }
        public string CityName { get; set; } = "";
        public DateTime Month { get; set; }
        public decimal Value { get; set; }
    }
}
