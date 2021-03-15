using System;
using System.Collections.Generic;
using System.Text;
using SushiBarBusinessLogic.Interfaces;
using SushiBarBusinessLogic.ViewModels;
using SushiBarBusinessLogic.BindingModels;

namespace SushiBarBusinessLogic.BusinessLogics
{
    public class KitchenLogic
    {
        private readonly IKitchenStorage _kitchenStorage;

        private readonly IIngredientStorage _ingredientStorage;

        public KitchenLogic(IKitchenStorage storeHouseStorage, IIngredientStorage ingredientStorage)
        {
            _kitchenStorage = storeHouseStorage;
            _ingredientStorage = ingredientStorage;
        }

        public List<KitchenViewModel> Read(KitchenBindingModel model)
        {
            if (model == null)
            {
                return _kitchenStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<KitchenViewModel> { _kitchenStorage.GetElement(model) };
            }
            return _kitchenStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(KitchenBindingModel model)
        {
            var element = _kitchenStorage.GetElement(new KitchenBindingModel { KitchenName = model.KitchenName });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            if (model.Id.HasValue)
            {
                _kitchenStorage.Update(model);
            }
            else
            {
                _kitchenStorage.Insert(model);
            }
        }

        public void Delete(KitchenBindingModel model)
        {
            var element = _kitchenStorage.GetElement(new KitchenBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Таковой кухни нет");
            }
            _kitchenStorage.Delete(model);
        }

        public void Refill(KitchenFillBindingModel model)
        {
            var kitchen = _kitchenStorage.GetElement(new KitchenBindingModel
            {
                Id = model.KitchenId
            });

            var ingredient = _ingredientStorage.GetElement(new IngredientBindingModel
            {
                Id = model.IngredientId
            });
            if (kitchen == null)
            {
                throw new Exception("Не найдена кухня");
            }
            if (ingredient == null)
            {
                throw new Exception("Не найден ингредиент");
            }
            if (kitchen.KitchenIngredients.ContainsKey(model.IngredientId))
            {
                kitchen.KitchenIngredients[model.IngredientId] =
                    (ingredient.IngredientName, kitchen.KitchenIngredients[model.IngredientId].Item2 + model.Count);
            }
            else
            {
                kitchen.KitchenIngredients.Add(ingredient.Id, (ingredient.IngredientName, model.Count));
            }
            _kitchenStorage.Update(new KitchenBindingModel
            {
                Id = kitchen.Id,
                KitchenName = kitchen.KitchenName,
                ResponsiblePersonFullName = kitchen.ResponsiblePersonFullName,
                DateCreate = kitchen.DateCreate,
                KitchenIngredients = kitchen.KitchenIngredients
            });
        }
    }
}
