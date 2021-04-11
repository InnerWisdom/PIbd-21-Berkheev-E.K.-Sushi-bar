using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using SushiBarBusinessLogic.Enums;
using SushiBarFileImplement.Models;
using System.Globalization;

namespace SushiBarFileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;

        private readonly string IngredientFileName = "Ingredient.xml";

        private readonly string OrderFileName = "Order.xml";

        private readonly string SushiFileName = "Sushi.xml";


        private readonly string KitchenFileName = "Kitchen.xml";

        public List<Ingredient> Ingredients { get; set; }

        public List<Order> Orders { get; set; }

        public List<Sushi> Sushis { get; set; }

        public List<Kitchen> Kitchens { get; set; }

        private FileDataListSingleton()
        {
            Ingredients = LoadIngredients();
            Orders = LoadOrders();
            Sushis = LoadSushis();
            Kitchens = LoadKitchens();
        }

        public static FileDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataListSingleton();
            }
            return instance;
        }

        ~FileDataListSingleton()
        {
            SaveIngredients();
            SaveOrders();
            SaveSushis();
            SaveKitchens();
        }

        private List<Ingredient> LoadIngredients()
        {
            var list = new List<Ingredient>();
            if (File.Exists(IngredientFileName))
            {
                XDocument xDocument = XDocument.Load(IngredientFileName);
                var xElements = xDocument.Root.Elements("Ingredient").ToList();

                foreach (var elem in xElements)
                {
                    list.Add(new Ingredient
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        IngredientName = elem.Element("IngredientName").Value
                    });
                }
            }
            return list;
        }

        private List<Order> LoadOrders()
        {
            var list = new List<Order>();
            if (File.Exists(OrderFileName))
            {
                XDocument xDocument = XDocument.Load(OrderFileName);
                var xElements = xDocument.Root.Elements("Order").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        SushiId = Convert.ToInt32(elem.Element("SushiId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value),
                        Sum = Convert.ToDecimal(elem.Element("Sum").Value),
                        Status = (OrderStatus)Convert.ToInt32(elem.Element("Status").Value),
                        DateCreate = Convert.ToDateTime(elem.Element("DateCreate").Value),
                        DateImplement = String.IsNullOrEmpty(elem.Element("DateImplement").Value) ? DateTime.MinValue : Convert.ToDateTime(elem.Element("DateImplement").Value),
                    });
                }
            }
            return list;
        }

        private List<Sushi> LoadSushis()
        {
            var list = new List<Sushi>();
            if (File.Exists(SushiFileName))
            {
                XDocument xDocument = XDocument.Load(SushiFileName);
                var xElements = xDocument.Root.Elements("Sushi").ToList();
                foreach (var elem in xElements)
                {
                    var sushiIngredient = new Dictionary<int, int>();
                    foreach (var ingredient in elem.Element("SushiIngredients").Elements("SushiIngredient").ToList())
                    {
                        sushiIngredient.Add(Convert.ToInt32(ingredient.Element("Key").Value), Convert.ToInt32(ingredient.Element("Value").Value));
                    }
                    list.Add(new Sushi
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        SushiName = elem.Element("SushiName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value),
                        SushiIngredients = sushiIngredient
                    });
                }
            }
            return list;
        }

        private List<Kitchen> LoadKitchens()
        {
            var list = new List<Kitchen>();

            if (File.Exists(KitchenFileName))
            {
                XDocument xDocument = XDocument.Load(KitchenFileName);

                var xElements = xDocument.Root.Elements("Kitchen").ToList();

                foreach (var kitchen in xElements)
                {
                    var kitchenIngredients = new Dictionary<int, int>();

                    foreach (var material in kitchen.Element("KitchenIngredients").Elements("KitchenIngredient").ToList())
                    {
                        kitchenIngredients.Add(Convert.ToInt32(material.Element("Key").Value), Convert.ToInt32(material.Element("Value").Value));
                    }

                    list.Add(new Kitchen
                    {
                        Id = Convert.ToInt32(kitchen.Attribute("Id").Value),
                        KitchenName = kitchen.Element("KitchenName").Value,
                        ResponsiblePersonFullName = kitchen.Element("ResponsiblePersonFullName").Value,
                        DateCreate = Convert.ToDateTime(kitchen.Element("DateCreate").Value),
                        KitchenIngredients = kitchenIngredients
                    });
                }
            }

            return list;
        }

        private void SaveKitchens()
        {
            if (Kitchens != null)
            {
                var xElement = new XElement("Kitchens");

                foreach (var kitchen in Kitchens)
                {
                    var ingredientElement = new XElement("KitchenIngredients");

                    foreach (var material in kitchen.KitchenIngredients)
                    {
                        ingredientElement.Add(new XElement("KitchenIngredient",
                            new XElement("Key", material.Key),
                            new XElement("Value", material.Value)));
                    }

                    xElement.Add(new XElement("Kitchen",
                        new XAttribute("Id", kitchen.Id),
                        new XElement("KitchenName", kitchen.KitchenName),
                        new XElement("ResponsiblePersonFullName", kitchen.ResponsiblePersonFullName),
                        new XElement("DateCreate", kitchen.DateCreate.ToString()),
                        ingredientElement));
                }

                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(KitchenFileName);
            }
        }

        private void SaveIngredients()
        {
            if (Ingredients != null)
            {
                var xElement = new XElement("Ingredients");
                foreach (var ingredient in Ingredients)
                {
                    xElement.Add(new XElement("Ingredient",
                    new XAttribute("Id", ingredient.Id),
                    new XElement("IngredientName", ingredient.IngredientName)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(IngredientFileName);
            }
        }

        private void SaveOrders()
        {
            if (Orders != null)
            {
                var xElement = new XElement("Orders");
                foreach (var order in Orders)
                {
                    xElement.Add(new XElement("Order",
                    new XAttribute("Id", order.Id),
                    new XElement("SushiId", order.SushiId),
                    new XElement("Count", order.Count),
                    new XElement("Sum", order.Sum),
                    new XElement("Status", (int)order.Status),
                    new XElement("DateCreate", order.DateCreate),
                    new XElement("DateImplement", order.DateImplement)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(OrderFileName);
            }
        }

        private void SaveSushis()
        {
            if (Sushis != null)
            {
                var xElement = new XElement("Sushis");
                foreach (var Sushi in Sushis)
                {
                    var ingElement = new XElement("SushiIngredients");
                    foreach (var ingredient in Sushi.SushiIngredients)
                    {
                        ingElement.Add(new XElement("SushiIngredient",
                        new XElement("Key", ingredient.Key),
                        new XElement("Value", ingredient.Value)));
                    }
                    xElement.Add(new XElement("Sushi",
                    new XAttribute("Id", Sushi.Id),
                    new XElement("SushiName", Sushi.SushiName),
                    new XElement("Price", Sushi.Price),
                    ingElement));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(SushiFileName);
            }
        }
    }
}
