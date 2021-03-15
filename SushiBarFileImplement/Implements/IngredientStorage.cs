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
    public class IngredientStorage : IIngredientStorage
    {
        private readonly FileDataListSingleton source;

        public IngredientStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public List<IngredientViewModel> GetFullList()
        {
            return source.Ingredients
            .Select(CreateModel)
            .ToList();
        }

        public List<IngredientViewModel> GetFilteredList(IngredientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Ingredients
            .Where(rec => rec.IngredientName.Contains(model.IngredientName))
            .Select(CreateModel)
            .ToList();
        }

        public IngredientViewModel GetElement(IngredientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var ingredient = source.Ingredients
            .FirstOrDefault(rec => rec.IngredientName == model.IngredientName || rec.Id == model.Id);
            return ingredient != null ? CreateModel(ingredient) : null;
        }

        public void Insert(IngredientBindingModel model)
        {
            int maxId = source.Ingredients.Count > 0 ? source.Ingredients.Max(rec => rec.Id) : 0;
            var element = new Ingredient { Id = maxId + 1 };
            source.Ingredients.Add(CreateModel(model, element));
        }

        public void Update(IngredientBindingModel model)
        {
            var element = source.Ingredients.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Ингредиент не найден");
            }
            CreateModel(model, element);
        }

        public void Delete(IngredientBindingModel model)
        {
            Ingredient element = source.Ingredients.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Ingredients.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        private Ingredient CreateModel(IngredientBindingModel model, Ingredient ingredient)
        {
            ingredient.IngredientName = model.IngredientName;
            return ingredient;
        }

        private IngredientViewModel CreateModel(Ingredient ingredient)
        {
            return new IngredientViewModel
            {
                Id = ingredient.Id,
                IngredientName = ingredient.IngredientName
            };
        }
    }
}
