using SushiBarBusinessLogic.BindingModels;
using SushiBarBusinessLogic.Interfaces;
using SushiBarBusinessLogic.ViewModels;
using SushiBarListImplement.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SushiBarListImplement.Implements
{
    public class SushiStorage : ISushiStorage
    {
        private readonly DataListSingleton source;
        public SushiStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<SushiViewModel> GetFullList()
        {
            List<SushiViewModel> result = new List<SushiViewModel>();
            foreach (var ingredient in source.Sushis)
            {
                result.Add(CreateModel(ingredient));
            }
            return result;
        }
        public List<SushiViewModel> GetFilteredList(SushiBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<SushiViewModel> result = new List<SushiViewModel>();
            foreach (var sushi in source.Sushis)
            {
                if (sushi.SushiName.Contains(model.SushiName))
                {
                    result.Add(CreateModel(sushi));
                }
            }
            return result;
        }
        public SushiViewModel GetElement(SushiBindingModel sample)
        {
            if (sample == null)
            {
                return null;
            }
            foreach (var sushi in source.Sushis)
            {
                if (sushi.Id == sample.Id || sushi.SushiName ==
                sample.SushiName)
                {
                    return CreateModel(sushi);
                }
            }
            return null;
        }
        public void Insert(SushiBindingModel model)
        {
            Sushi tempSushi = new Sushi
            {
                Id = 1,
                SushiIngredients = new
            Dictionary<int, int>()
            };
            foreach (var sushi in source.Sushis)
            {
                if (sushi.Id >= tempSushi.Id)
                {
                    tempSushi.Id = sushi.Id + 1;
                }
            }
            source.Sushis.Add(CreateSample(model, tempSushi));
        }
        public void Update(SushiBindingModel model)
        {
            Sushi tempSushi = null;
            foreach (var sushi in source.Sushis)
            {
                if (sushi.Id == model.Id)
                {
                    tempSushi = sushi;
                }
            }
            if (tempSushi == null)
            {
                throw new Exception("Суши не найдены");
            }
            CreateSample(model, tempSushi);
        }
        public void Delete(SushiBindingModel sample)
        {
            for (int i = 0; i < source.Sushis.Count; ++i)
            {
                if (source.Sushis[i].Id == sample.Id)
                {
                    source.Sushis.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Суши не найдены");
        }
        private Sushi CreateSample(SushiBindingModel sample, Sushi sushi)
        {
            sushi.SushiName = sample.SushiName;
            sushi.Price = sample.Price;
            // удаляем убранные
            foreach (var key in sushi.SushiIngredients.Keys.ToList())
            {
                if (!sample.SushiIngredients.ContainsKey(key))
                {
                    sushi.SushiIngredients.Remove(key);
                }
            }
            // обновляем существуюущие и добавляем новые
            foreach (var ingredient in sample.SushiIngredients)
            {
                if (sushi.SushiIngredients.ContainsKey(ingredient.Key))
                {
                    sushi.SushiIngredients[ingredient.Key] =
                    sample.SushiIngredients[ingredient.Key].Item2;
                }
                else
                {
                    sushi.SushiIngredients.Add(ingredient.Key,
                    sample.SushiIngredients[ingredient.Key].Item2);
                }
            }
            return sushi;
        }
        private SushiViewModel CreateModel(Sushi sushi)
        {
            // требуется дополнительно получить список компонентов для изделия с названиями и их количество
        Dictionary<int, (string, int)> sushiIngredients = new
        Dictionary<int, (string, int)>();
            foreach (var pc in sushi.SushiIngredients)
            {
                string ingredientName = string.Empty;
                foreach (var ingredient in source.Ingredients)
                {
                    if (pc.Key == ingredient.Id)
                    {
                        ingredientName = ingredient.IngredientName;
                        break;
                    }
                }
                sushiIngredients.Add(pc.Key, (ingredientName, pc.Value));
            }
            return new SushiViewModel
            {
                Id = sushi.Id,
                SushiName = sushi.SushiName,
                Price = sushi.Price,
                SushiIngredients = sushiIngredients
            };
        }
    }
}
