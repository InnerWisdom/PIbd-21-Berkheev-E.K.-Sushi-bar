﻿using System.Collections.Generic;
using SushiBarListImplement.Models;

namespace SushiBarListImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;
        
        public List<Ingredient> Ingredients { get; set; }
        
        public List<Order> Orders { get; set; }
        
        public List<Sushi> Sushis { get; set; }

        public List<Kitchen> Kitchens { get; set; }



        public List<Client> Clients { get; set; }

        private DataListSingleton()
        {
            Ingredients = new List<Ingredient>();
            Orders = new List<Order>();
            Sushis = new List<Sushi>();
            Kitchens = new List<Kitchen>();
            Clients = new List<Client>();
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
