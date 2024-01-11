namespace Basket.Host.Models
{
    public class Item
    {
        private static int _id = 0;

        public int Id { get; private set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public Item()
        {
            Id = ++_id;
        }


    }
}
