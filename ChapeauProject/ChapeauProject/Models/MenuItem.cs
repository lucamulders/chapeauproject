namespace ChapeauProject.Models
{
    public class MenuItem
    {
        public int MenuItemID { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; } 
        public string CourseName { get; set; }  
        public string MenuCard { get; set; }    

        public MenuItem(int id, string name, decimal price, int stock, string course, string card)
        {
            MenuItemID = id;
            ItemName = name;
            Price = price;
            StockQuantity = stock;
            CourseName = course;
            MenuCard = card;
        }
    }
}
//changr to menu 
//enum for categpry 