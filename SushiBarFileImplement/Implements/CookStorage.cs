using System;
using System.Collections.Generic;
using System.Linq;
using SushiBarBusinessLogic.BindingModels;
using SushiBarBusinessLogic.Interfaces;
using SushiBarBusinessLogic.ViewModels;
using SushiBarFileImplement.Models;

namespace SushiBarFileImplement.Implements
{
    public class CookStorage : ICookStorage
    {
        private readonly FileDataListSingleton source;
        public CookStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public List<CookViewModel> GetFullList()
        {
            return source.Cooks
            .Select(CreateModel)
            .ToList();
        }
        public List<CookViewModel> GetFilteredList(CookBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Cooks
            .Where(rec => rec.CookFIO.Contains(model.CookFIO))
            .Select(CreateModel)
            .ToList();
        }
        public CookViewModel GetElement(CookBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var cook = source.Cooks
            .FirstOrDefault(rec => rec.Id == model.Id);
            return cook != null ? CreateModel(cook) : null;
        }
        public void Insert(CookBindingModel model)
        {
            int maxId = source.Cooks.Count > 0 ? source.Cooks.Max(rec => rec.Id) : 0;
            var element = new Cook { Id = maxId + 1 };
            source.Cooks.Add(CreateModel(model, element));
        }
        public void Update(CookBindingModel model)
        {
            var element = source.Cooks.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
        }
        public void Delete(CookBindingModel model)
        {
            Cook element = source.Cooks.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Cooks.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private Cook CreateModel(CookBindingModel model, Cook cook)
        {
            cook.CookFIO = model.CookFIO;
            cook.WorkingTime = model.WorkingTime;
            cook.PauseTime = model.PauseTime;
            return cook;
        }

        private CookViewModel CreateModel(Cook cook)
        {
            return new CookViewModel
            {
                Id = cook.Id,
                CookFIO = cook.CookFIO,
                WorkingTime = cook.WorkingTime,
                PauseTime = cook.PauseTime
            };
        }
    }
}
