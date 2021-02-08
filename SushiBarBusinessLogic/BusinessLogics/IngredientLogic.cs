using SushiBarBusinessLogic.BindingModels;
using SushiBarBusinessLogic.Interfaces;
using SushiBarBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;


namespace SushiBarBusinessLogic.BusinessLogics
{
    public class IngredientLogic
    {
        private readonly IIngredientStorage _ingredientStorage;
        public IngredientLogic(IIngredientStorage ingredientStorage)
        {
            _ingredientStorage = ingredientStorage;
        }
        public List<IngredientViewModel> Read(IngredientBindingModel sample)
        {
            if (sample == null)
            {
                return _ingredientStorage.GetFullList();
            }
            if (sample.Id.HasValue)
            {
                return new List<IngredientViewModel> { _ingredientStorage.GetElement(sample)
};
            }
            return _ingredientStorage.GetFilteredList(sample);
        }
        public void CreateOrUpdate(IngredientBindingModel model)
        {
            var element = _ingredientStorage.GetElement(new IngredientBindingModel
            {
                IngredientName = model.IngredientName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            if (model.Id.HasValue)
            {
                _ingredientStorage.Update(model);
            }
            else
            {
                _ingredientStorage.Insert(model);
            }
        }
        public void Delete(IngredientBindingModel model)
        {
            var element = _ingredientStorage.GetElement(new IngredientBindingModel
            {
                Id =
           model.Id
            });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _ingredientStorage.Delete(model);
        }
    }
}
