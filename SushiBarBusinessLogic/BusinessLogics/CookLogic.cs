using System;
using System.Collections.Generic;
using SushiBarBusinessLogic.BindingModels;
using SushiBarBusinessLogic.Interfaces;
using SushiBarBusinessLogic.ViewModels;

namespace SushiBarBusinessLogic.BusinessLogics
{
    public class CookLogic
    {
        private readonly ICookStorage _clientStorage;

        public CookLogic(ICookStorage clientStorage)
        {
            _clientStorage = clientStorage;
        }

        public List<CookViewModel> Read(CookBindingModel model)
        {
            if (model == null)
            {
                return _clientStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<CookViewModel> { _clientStorage.GetElement(model) };
            }
            return _clientStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(CookBindingModel model)
        {
            var element = _clientStorage.GetElement(new CookBindingModel
            {
                CookFIO = model.CookFIO
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть работник с таким ФИО");
            }
            if (model.Id.HasValue)
            {
                _clientStorage.Update(model);
            }
            else
            {
                _clientStorage.Insert(model);
            }
        }

        public void Delete(CookBindingModel model)
        {
            var element = _clientStorage.GetElement(new CookBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Работник не найден");
            }
            _clientStorage.Delete(model);
        }
    }
}
