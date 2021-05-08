using System.Collections.Generic;
using SushiBarListImplement.Models;

namespace SushiBarListImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;
        
        public List<Ingredient> Ingredients { get; set; }
        
        public List<Order> Orders { get; set; }
        
        public List<Sushi> Sushis { get; set; }

        public List<Client> Clients { get; set; }

        public List<Cook> Cooks { get; set; }

        private DataListSingleton()
        {
            Ingredients = new List<Ingredient>();
            Orders = new List<Order>();
            Sushis = new List<Sushi>();
            Clients = new List<Client>();
            Cooks = new List<Cook>();
        }
        
        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }

}
