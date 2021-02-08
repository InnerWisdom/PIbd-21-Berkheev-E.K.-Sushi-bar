using SushiBarBusinessLogic.BindingModels;
using SushiBarBusinessLogic.Interfaces;
using SushiBarBusinessLogic.ViewModels;
using SushiBarListImplement.Models;
using System;
using System.Collections.Generic;


namespace SushiBarListImplement.Implements
{
    public class IngredientStorage : IIngredientStorage
    {
        private readonly DataListSingleton source;
        public IngredientStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<IngredientViewModel> GetFullList()
        {
            List<IngredientViewModel> result = new List<IngredientViewModel>();
            foreach (var ingredient in source.Ingredients)
            {
                result.Add(CreateSample(ingredient));
            }
            return result;
        }
        public List<IngredientViewModel> GetFilteredList(IngredientBindingModel sample)
        {
            if (sample == null)
            {
                return null;
            }
            List<IngredientViewModel> result = new List<IngredientViewModel>();
            foreach (var ingredient in source.Ingredients)
            {
                if (ingredient.IngredientName.Contains(sample.IngredientName))
                {
                    result.Add(CreateSample(ingredient));
                }
            }
            return result;
        }
        public IngredientViewModel GetElement(IngredientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var ingredient in source.Ingredients)
            {
                if (ingredient.Id == model.Id || ingredient.IngredientName ==
               model.IngredientName)
                {
                    return CreateSample(ingredient);
                }
            }
            return null;
        }
        public void Insert(IngredientBindingModel sample)
        {
            Ingredient tempIngredient = new Ingredient { Id = 1 };
            foreach (var ingredient in source.Ingredients)
            {
                if (ingredient.Id >= tempIngredient.Id)
                {
                    tempIngredient.Id = ingredient.Id + 1;
                }
            }
            source.Ingredients.Add(CreateSample(sample, tempIngredient));
        }
        public void Update(IngredientBindingModel sample)
        {
            Ingredient tempIngredient = null;
            foreach (var ingredient in source.Ingredients)
            {
                if (ingredient.Id == sample.Id)
                {
                    tempIngredient = ingredient;
                }
            }
            if (tempIngredient == null)
            {
                throw new Exception("Ингредиент не найден");
            }
            CreateSample(sample, tempIngredient);
        }
        public void Delete(IngredientBindingModel model)
        {
            for (int i = 0; i < source.Ingredients.Count; ++i)
            {
                if (source.Ingredients[i].Id == model.Id.Value)
                {
                    source.Ingredients.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Ингредиент не найден");
        }
        private Ingredient CreateSample(IngredientBindingModel sample, Ingredient ingredient)
        {
            ingredient.IngredientName = sample.IngredientName;
            return ingredient;
        }
        private IngredientViewModel CreateSample(Ingredient ingredient)
        {
            return new IngredientViewModel
            {
                Id = ingredient.Id,
                IngredientName = ingredient.IngredientName
            };
        }
    }
}
