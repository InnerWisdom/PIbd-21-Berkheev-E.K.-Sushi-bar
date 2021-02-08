using SushiBarBusinessLogic.BindingModels;
using SushiBarBusinessLogic.Interfaces;
using SushiBarBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;


namespace SushiBarBusinessLogic.BusinessLogics
{
    public class SushiLogic
    {
        private readonly ISushiStorage _productStorage;
        public SushiLogic(ISushiStorage productStorage)
        {
            _productStorage = productStorage;
        }
        public List<SushiViewModel> Read(SushiBindingModel model)
        {
            if (model == null)
            {
                return _productStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<SushiViewModel> { _productStorage.GetElement(model)
};
            }
            return _productStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(SushiBindingModel model)
        {
            var element = _productStorage.GetElement(new SushiBindingModel
            {
                SushiName = model.SushiName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            if (model.Id.HasValue)
            {
                _productStorage.Update(model);
            }
            else
            {
                _productStorage.Insert(model);
            }
        }
        public void Delete(SushiBindingModel model)
        {
            var element = _productStorage.GetElement(new SushiBindingModel
            {
                Id =
           model.Id
            });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _productStorage.Delete(model);
        }
    }
}
