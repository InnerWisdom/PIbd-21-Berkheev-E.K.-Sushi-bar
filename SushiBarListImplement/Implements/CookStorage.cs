using System;
using System.Collections.Generic;
using SushiBarBusinessLogic.BindingModels;
using SushiBarBusinessLogic.Interfaces;
using SushiBarBusinessLogic.ViewModels;
using SushiBarListImplement.Models;

namespace SushiBarListImplement.Implements
{
    public class CookStorage : ICookStorage
    {
        private readonly DataListSingleton source;

        public CookStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<CookViewModel> GetFullList()
        {
            List<CookViewModel> result = new List<CookViewModel>();
            foreach (var cook in source.Cooks)
            {
                result.Add(CreateModel(cook));
            }
            return result;
        }

        public List<CookViewModel> GetFilteredList(CookBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<CookViewModel> result = new List<CookViewModel>();
            foreach (var cook in source.Cooks)
            {
                if (cook.CookFIO.Contains(model.CookFIO))
                {
                    result.Add(CreateModel(cook));
                }
            }
            if (result.Count > 0)
            {
                return result;
            }
            return null;
        }

        public CookViewModel GetElement(CookBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var cook in source.Cooks)
            {
                if (cook.Id == model.Id)
                {
                    return CreateModel(cook);
                }
            }
            return null;
        }

        public void Insert(CookBindingModel model)
        {
            Cook tempCook = new Cook { Id = 1 };
            foreach (var cook in source.Cooks)
            {
                if (cook.Id >= tempCook.Id)
                {
                    tempCook.Id = cook.Id + 1;
                }
            }
            source.Cooks.Add(CreateModel(model, tempCook));
        }

        public void Update(CookBindingModel model)
        {
            Cook tempCook = null;
            foreach (var cook in source.Cooks)
            {
                if (cook.Id == model.Id)
                {
                    tempCook = cook;
                }
            }
            if (tempCook == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempCook);
        }

        public void Delete(CookBindingModel model)
        {
            for (int i = 0; i < source.Cooks.Count; ++i)
            {
                if (source.Cooks[i].Id == model.Id.Value)
                {
                    source.Cooks.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        private Cook CreateModel(CookBindingModel model, Cook cook)
        {
            cook.CookFIO = model.CookFIO;
            cook.PauseTime = model.PauseTime;
            cook.WorkingTime = model.WorkingTime;
            return cook;
        }

        private CookViewModel CreateModel(Cook cook)
        {
            return new CookViewModel
            {
                Id = cook.Id,
                CookFIO = cook.CookFIO,
                PauseTime = cook.PauseTime,
                WorkingTime = cook.WorkingTime
            };
        }
    }
}
