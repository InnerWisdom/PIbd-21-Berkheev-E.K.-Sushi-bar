using System;
using System.Collections.Generic;
using System.Linq;
using SushiBarBusinessLogic.BindingModels;
using SushiBarBusinessLogic.Interfaces;
using SushiBarBusinessLogic.ViewModels;
using SushiBarDatabaseImplement.Models;

namespace SushiBarDatabaseImplement.Implements
{
    public class CookStorage : ICookStorage
    {
        public List<CookViewModel> GetFullList()
        {
            using (var context = new SushiBarDatabase())
            {
                return context.Cooks.Select(rec => new CookViewModel
                {
                    Id = rec.Id,
                    CookFIO = rec.CookFIO,
                    WorkingTime = rec.WorkingTime,
                    PauseTime = rec.PauseTime,
                })
                .ToList();
            }
        }

        public List<CookViewModel> GetFilteredList(CookBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new SushiBarDatabase())
            {
                return context.Cooks
                .Where(rec => rec.CookFIO.Contains(model.CookFIO))
                .Select(rec => new CookViewModel
                {
                    Id = rec.Id,
                    CookFIO = rec.CookFIO,
                    WorkingTime = rec.WorkingTime,
                    PauseTime = rec.PauseTime,
                })
                .ToList();
            }
        }

        public CookViewModel GetElement(CookBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new SushiBarDatabase())
            {
                var cook = context.Cooks
                .FirstOrDefault(rec => rec.CookFIO == model.CookFIO ||
                rec.Id == model.Id);
                return cook != null ?
                new CookViewModel
                {
                    Id = cook.Id,
                    CookFIO = cook.CookFIO,
                    WorkingTime = cook.WorkingTime,
                    PauseTime = cook.PauseTime,
                } :
                null;
            }
        }

        public void Insert(CookBindingModel model)
        {
            using (var context = new SushiBarDatabase())
            {
                context.Cooks.Add(CreateModel(model, new Cook(), context));
                context.SaveChanges();
            }
        }

        public void Update(CookBindingModel model)
        {
            using (var context = new SushiBarDatabase())
            {
                var element = context.Cooks.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Работник не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();
            }
        }

        public void Delete(CookBindingModel model)
        {
            using (var context = new SushiBarDatabase())
            {
                Cook element = context.Cooks.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Cooks.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Работник не найден");
                }
            }
        }

        private Cook CreateModel(CookBindingModel model, Cook cook, SushiBarDatabase database)
        {
            cook.CookFIO = model.CookFIO;
            cook.WorkingTime = model.WorkingTime;
            cook.PauseTime = model.PauseTime;
            return cook;
        }
    }
}
