using System;
using System.Collections.Generic;
using SushiBarBusinessLogic.BindingModels;
using SushiBarBusinessLogic.Interfaces;
using SushiBarBusinessLogic.ViewModels;
using SushiBarListImplement.Models;
using System.Linq;

namespace SushiBarListImplement.Implements
{
    public class KitchenStorage : IKitchenStorage
    {
        private readonly DataListSingleton source;

        public KitchenStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<KitchenViewModel> GetFullList()
        {
            List<KitchenViewModel> result = new List<KitchenViewModel>();
            foreach (var kitchen in source.Kitchens)
            {
                result.Add(CreateModel(kitchen));
            }
            return result;
        }

        public List<KitchenViewModel> GetFilteredList(KitchenBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<KitchenViewModel> result = new List<KitchenViewModel>();
            foreach (var kitchen in source.Kitchens)
            {
                if (kitchen.KitchenName.Contains(model.KitchenName))
                {
                    result.Add(CreateModel(kitchen));
                }
            }
            return result;
        }

        public KitchenViewModel GetElement(KitchenBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var kitchen in source.Kitchens)
            {
                if (kitchen.Id == model.Id || kitchen.KitchenName == model.KitchenName)
                {
                    return CreateModel(kitchen);
                }
            }
            return null;
        }

        public void Insert(KitchenBindingModel model)
        {
            Kitchen tempKitchen = new Kitchen
            {
                Id = 1,
                KitchenIngredients = new Dictionary<int, int>(),
                DateCreate = DateTime.Now
            };
            foreach (var kitchen in source.Kitchens)
            {
                if (kitchen.Id >= tempKitchen.Id)
                {
                    tempKitchen.Id = kitchen.Id + 1;
                }
            }
            source.Kitchens.Add(CreateModel(model, tempKitchen));
        }

        public void Update(KitchenBindingModel model)
        {
            Kitchen tempKitchen = null;
            foreach (var Kitchen in source.Kitchens)
            {
                if (Kitchen.Id == model.Id)
                {
                    tempKitchen = Kitchen;
                }
            }
            if (tempKitchen == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempKitchen);
        }

        public void Delete(KitchenBindingModel model)
        {
            for (int i = 0; i < source.Kitchens.Count; ++i)
            {
                if (source.Kitchens[i].Id == model.Id)
                {
                    source.Kitchens.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        private Kitchen CreateModel(KitchenBindingModel model, Kitchen kitchen)
        {
            kitchen.KitchenName = model.KitchenName;
            kitchen.ResponsiblePersonFullName = model.ResponsiblePersonFullName;
            // удаляем убранные
            foreach (var key in kitchen.KitchenIngredients.Keys.ToList())
            {
                if (!model.KitchenIngredients.ContainsKey(key))
                {
                    kitchen.KitchenIngredients.Remove(key);
                }
            }
            // обновляем существуюущие и добавляем новые
            foreach (var ingredient in model.KitchenIngredients)
            {
                if (kitchen.KitchenIngredients.ContainsKey(ingredient.Key))
                {
                    kitchen.KitchenIngredients[ingredient.Key] = model.KitchenIngredients[ingredient.Key].Item2;
                }
                else
                {
                    kitchen.KitchenIngredients.Add(ingredient.Key, model.KitchenIngredients[ingredient.Key].Item2);
                }
            }
            return kitchen;
        }

        private KitchenViewModel CreateModel(Kitchen kitchen)
        {
            // требуется дополнительно получить список компонентов для изделия с названиями и их количество
            Dictionary<int, (string, int)> kitchenIngredients = new Dictionary<int, (string, int)>();
            foreach (var sc in kitchen.KitchenIngredients)
            {
                string ingredientName = string.Empty;
                foreach (var ingredient in source.Ingredients)
                {
                    if (sc.Key == ingredient.Id)
                    {
                        ingredientName = ingredient.IngredientName;
                        break;
                    }
                }
                kitchenIngredients.Add(sc.Key, (ingredientName, sc.Value));
            }
            return new KitchenViewModel
            {
                Id = kitchen.Id,
                KitchenName = kitchen.KitchenName,
                ResponsiblePersonFullName = kitchen.ResponsiblePersonFullName,
                DateCreate = kitchen.DateCreate,
                KitchenIngredients = kitchenIngredients
            };
        }

        public void Print()
        {
            foreach (Kitchen kitchen in source.Kitchens)
            {
                Console.WriteLine(kitchen.KitchenName + " " + kitchen.ResponsiblePersonFullName + " " + kitchen.DateCreate);
                foreach (KeyValuePair<int, int> keyValue in kitchen.KitchenIngredients)
                {
                    string ingredientName = source.Ingredients.FirstOrDefault(component => component.Id == keyValue.Key).IngredientName;
                    Console.WriteLine(ingredientName + " " + keyValue.Value);
                }
            }
        }
        public bool TakeFromKitchen(Dictionary<int, (string, int)> ingredients, int sushiCount)
        {
            throw new NotImplementedException();
        }
    }
}
