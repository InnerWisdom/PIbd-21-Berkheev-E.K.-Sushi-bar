using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using SushiBarBusinessLogic.Enums;
using SushiBarFileImplement.Models;

namespace SushiBarFileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;

        private readonly string IngredientFileName = "Ingredient.xml";

        private readonly string OrderFileName = "Order.xml";

        private readonly string SushiFileName = "Sushi.xml";


        private readonly string ClientFileName = "Client.xml";
        public List<Ingredient> Ingredients { get; set; }

        public List<Order> Orders { get; set; }

        public List<Sushi> Sushis { get; set; }

        public List<Client> Clients { get; set; }

        public List<Cook> Cooks { get; set; }
        private FileDataListSingleton()
        {
            Ingredients = LoadIngredients();
            Orders = LoadOrders();
            Sushis = LoadSushis();
            Clients = LoadClients();
            Cooks = new List<Cook>();
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
            SaveClients();
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
                        ClientId = Convert.ToInt32(elem.Element("ClientId")?.Value),
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

        private List<Client> LoadClients()
        {
            var list = new List<Client>();
            if (File.Exists(ClientFileName))
            {
                XDocument xDocument = XDocument.Load(ClientFileName);
                var xElements = xDocument.Root.Elements("Clients").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Client
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ClientFIO = elem.Element("ClientFIO").Value,
                        Email = elem.Element("Email").Value,
                        Password = elem.Element("Password").Value
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
                    new XElement("ClientId", order.ClientId),
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


        private void SaveClients()
        {
            if (Clients != null)
            {
                var xElement = new XElement("Clients");
                foreach (var client in Clients)
                {
                    xElement.Add(new XElement("Client",
                    new XAttribute("Id", client.Id),
                    new XElement("ClientFIO", client.ClientFIO),
                    new XElement("Email", client.Email),
                    new XElement("Password", client.Password)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ClientFileName);
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
