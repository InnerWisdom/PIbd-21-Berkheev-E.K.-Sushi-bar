using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using SushiBarBusinessLogic.BindingModels;
using SushiBarBusinessLogic.Interfaces;
using SushiBarBusinessLogic.ViewModels;
using SushiBarFileImplement.Models;

namespace SushiBarFileImplement.Implements
{
    public class KitchenStorage : IKitchenStorage
    {
        private readonly FileDataListSingleton source;

        public KitchenStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }

        private Kitchen CreateModel(KitchenBindingModel model, Kitchen kitchen)
        {
            kitchen.KitchenName = model.KitchenName;
            kitchen.ResponsiblePersonFullName = model.ResponsiblePersonFullName;

            foreach (var key in kitchen.KitchenIngredients.Keys.ToList())
            {
                if (!model.KitchenIngredients.ContainsKey(key))
                {
                    kitchen.KitchenIngredients.Remove(key);
                }
            }

            foreach (var material in model.KitchenIngredients)
            {
                if (kitchen.KitchenIngredients.ContainsKey(material.Key))
                {
                    kitchen.KitchenIngredients[material.Key] =
                        model.KitchenIngredients[material.Key].Item2;
                }
                else
                {
                    kitchen.KitchenIngredients.Add(material.Key, model.KitchenIngredients[material.Key].Item2);
                }
            }

            return kitchen;
        }

        private KitchenViewModel CreateModel(Kitchen kitchen)
        {
            Dictionary<int, (string, int)> kitchenIngredients = new Dictionary<int, (string, int)>();

            foreach (var kitchenIngredient in kitchen.KitchenIngredients)
            {
                string materialName = string.Empty;
                foreach (var material in source.Ingredients)
                {
                    if (kitchenIngredient.Key == material.Id)
                    {
                        materialName = material.IngredientName;
                        break;
                    }
                }
                kitchenIngredients.Add(kitchenIngredient.Key, (materialName, kitchenIngredient.Value));
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

        public List<KitchenViewModel> GetFullList()
        {
            return source.Kitchens.Select(CreateModel).ToList();
        }

        public List<KitchenViewModel> GetFilteredList(KitchenBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            return source.Kitchens.Where(xKitchen => xKitchen.KitchenName.Contains(model.KitchenName)).Select(CreateModel).ToList();
        }

        public KitchenViewModel GetElement(KitchenBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            var kitchen = source.Kitchens.FirstOrDefault(xKitchen => xKitchen.KitchenName == model.KitchenName || xKitchen.Id == model.Id);

            return kitchen != null ? CreateModel(kitchen) : null;
        }

        public void Insert(KitchenBindingModel model)
        {
            int maxId = source.Kitchens.Count > 0 ? source.Kitchens.Max(xKitchen => xKitchen.Id) : 0;
            var kitchen = new Kitchen { Id = maxId + 1, KitchenIngredients = new Dictionary<int, int>(), DateCreate = DateTime.Now };
            source.Kitchens.Add(CreateModel(model, kitchen));
        }

        public void Update(KitchenBindingModel model)
        {
            var kitchen = source.Kitchens.FirstOrDefault(xKitchen => xKitchen.Id == model.Id);

            if (kitchen == null)
            {
                throw new Exception("Склад не найден");
            }

            CreateModel(model, kitchen);
        }

        public void Delete(KitchenBindingModel model)
        {
            var kitchen = source.Kitchens.FirstOrDefault(xKitchen => xKitchen.Id == model.Id);

            if (kitchen != null)
            {
                source.Kitchens.Remove(kitchen);
            }
            else
            {
                throw new Exception("Склад не найден");
            }
        }


        public bool CheckIngredientsCount(int count, Dictionary<int, (string, int)> ingredients)
        {
            foreach (var ingredient in ingredients)
            {
                int compCount = source.Kitchens.Where(wh => wh.KitchenIngredients.ContainsKey(ingredient.Key))
                    .Sum(wh => wh.KitchenIngredients[ingredient.Key]);
                if (compCount < (ingredient.Value.Item2 * count))
                {
                    return false;
                }
            }
            foreach (var ingredient in ingredients)
            {
                int requiredCount = ingredient.Value.Item2 * count;
                while (requiredCount > 0)
                {
                    var kitchen = source.Kitchens
                        .FirstOrDefault(wh => wh.KitchenIngredients.ContainsKey(ingredient.Key)
                        && wh.KitchenIngredients[ingredient.Key] > 0);
                    int availableCount = kitchen.KitchenIngredients[ingredient.Key];
                    requiredCount -= availableCount;
                    kitchen.KitchenIngredients[ingredient.Key] = (requiredCount < 0) ? availableCount - (availableCount + requiredCount) : 0;
                }
            }
            return true;
        }

    }
}
