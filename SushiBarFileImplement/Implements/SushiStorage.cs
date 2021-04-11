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
    public class SushiStorage : ISushiStorage
    {
        private readonly FileDataListSingleton source;

        public SushiStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public List<SushiViewModel> GetFullList()
        {
            return source.Sushis
            .Select(CreateModel)
            .ToList();
        }

        public List<SushiViewModel> GetFilteredList(SushiBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Sushis
            .Where(rec => rec.SushiName.Contains(model.SushiName))
            .Select(CreateModel)
            .ToList();
        }

        public SushiViewModel GetElement(SushiBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var Sushi = source.Sushis
            .FirstOrDefault(rec => rec.SushiName == model.SushiName || rec.Id == model.Id);
            return Sushi != null ? CreateModel(Sushi) : null;
        }

        public void Insert(SushiBindingModel model)
        {
            int maxId = source.Sushis.Count > 0 ? source.Sushis.Max(rec => rec.Id) : 0;
            var element = new Sushi { Id = maxId + 1, SushiIngredients = new Dictionary<int, int>() };
            source.Sushis.Add(CreateModel(model, element));
        }

        public void Update(SushiBindingModel model)
        {
            var element = source.Sushis.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
        }

        public void Delete(SushiBindingModel model)
        {
            Sushi element = source.Sushis.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Sushis.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        private Sushi CreateModel(SushiBindingModel model, Sushi Sushi)
        {
            Sushi.SushiName = model.SushiName;
            Sushi.Price = model.Price;
            // удаляем убранные
            foreach (var key in Sushi.SushiIngredients.Keys.ToList())
            {
                if (!model.SushiIngredients.ContainsKey(key))
                {
                    Sushi.SushiIngredients.Remove(key);
                }
            }
            // обновляем существуюущие и добавляем новые
            foreach (var ingredient in model.SushiIngredients)
            {
                if (Sushi.SushiIngredients.ContainsKey(ingredient.Key))
                {
                    Sushi.SushiIngredients[ingredient.Key] = model.SushiIngredients[ingredient.Key].Item2;
                }
                else
                {
                    Sushi.SushiIngredients.Add(ingredient.Key, model.SushiIngredients[ingredient.Key].Item2);
                }
            }
            return Sushi;
        }

        private SushiViewModel CreateModel(Sushi Sushi)
        {
            return new SushiViewModel
            {
                Id = Sushi.Id,
                SushiName = Sushi.SushiName,
                Price = Sushi.Price,
                SushiIngredients = Sushi.SushiIngredients.ToDictionary(recPC => recPC.Key, recPC =>
                (source.Ingredients.FirstOrDefault(recC => recC.Id == recPC.Key)?.IngredientName, recPC.Value))
            };
        }
    }
}
