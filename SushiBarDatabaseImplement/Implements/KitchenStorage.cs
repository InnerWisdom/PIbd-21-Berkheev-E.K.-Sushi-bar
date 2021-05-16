using SushiBarBusinessLogic.BindingModels;
using SushiBarBusinessLogic.Interfaces;
using SushiBarBusinessLogic.ViewModels;
using SushiBarDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SushiBarDatabaseImplement.Implements
{
    public class KitchenStorage : IKitchenStorage
    {
        public List<KitchenViewModel> GetFullList()
        {
            using (var context = new SushiBarDatabase())
            {
                return context.Kitchens
                    .Include(rec => rec.KitchenIngredients)
                    .ThenInclude(rec => rec.Ingredient)
                    .ToList().Select(rec => new KitchenViewModel
                    {
                        Id = rec.Id,
                        KitchenName = rec.KitchenName,
                        ResponsiblePersonFullName = rec.ResponsiblePersonFullName,
                        DateCreate = rec.DateCreate,
                        KitchenIngredients = rec.KitchenIngredients
                            .ToDictionary(recPPC => recPPC.IngredientId,
                            recPPC => (recPPC.Ingredient?.IngredientName, recPPC.Count))
                    })
                    .ToList();
            }
        }

        public List<KitchenViewModel> GetFilteredList(KitchenBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new SushiBarDatabase())
            {
                return context.Kitchens
                    .Include(rec => rec.KitchenIngredients)
                    .ThenInclude(rec => rec.Ingredient)
                    .Where(rec => rec.KitchenName
                    .Contains(model.KitchenName))
                    .ToList()
                    .Select(rec => new KitchenViewModel
                    {
                        Id = rec.Id,
                        KitchenName = rec.KitchenName,
                        ResponsiblePersonFullName = rec.ResponsiblePersonFullName,
                        DateCreate = rec.DateCreate,
                        KitchenIngredients = rec.KitchenIngredients
                            .ToDictionary(recPC => recPC.IngredientId, recPC => (recPC.Ingredient?.IngredientName, recPC.Count))
                    })
                    .ToList();
            }
        }

        public KitchenViewModel GetElement(KitchenBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new SushiBarDatabase())
            {
                var kitchen = context.Kitchens
                    .Include(rec => rec.KitchenIngredients)
                    .ThenInclude(rec => rec.Ingredient)
                    .FirstOrDefault(rec => rec.KitchenName == model.KitchenName || rec.Id == model.Id);

                return kitchen != null ?
                    new KitchenViewModel
                    {
                        Id = kitchen.Id,
                        KitchenName = kitchen.KitchenName,
                        ResponsiblePersonFullName = kitchen.ResponsiblePersonFullName,
                        DateCreate = kitchen.DateCreate,
                        KitchenIngredients = kitchen.KitchenIngredients
                            .ToDictionary(recPC => recPC.IngredientId, recPC => (recPC.Ingredient?.IngredientName, recPC.Count))
                    } :
                    null;
            }
        }

        public void Insert(KitchenBindingModel model)
        {
            using (var context = new SushiBarDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        model.DateCreate = DateTime.Now;
                        CreateModel(model, new Kitchen(), context);
                        context.SaveChanges();

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();

                        throw;
                    }
                }
            }
        }

        public void Update(KitchenBindingModel model)
        {
            using (var context = new SushiBarDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Kitchens.FirstOrDefault(rec => rec.Id == model.Id);

                        if (element == null)
                        {
                            throw new Exception("Элемент не найден");
                        }

                        CreateModel(model, element, context);
                        context.SaveChanges();

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();

                        throw;
                    }
                }
            }
        }

        public void Delete(KitchenBindingModel model)
        {
            using (var context = new SushiBarDatabase())
            {
                Kitchen element = context.Kitchens.FirstOrDefault(rec => rec.Id == model.Id);

                if (element != null)
                {
                    context.Kitchens.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }



        private Kitchen CreateModel(KitchenBindingModel model, Kitchen kitchen, SushiBarDatabase context)
        {
            kitchen.KitchenName = model.KitchenName;
            kitchen.ResponsiblePersonFullName = model.ResponsiblePersonFullName;
            kitchen.DateCreate = model.DateCreate;

            if (kitchen.Id == 0)
            {
                context.Kitchens.Add(kitchen);
                context.SaveChanges();
            }

            if (model.Id.HasValue)
            {
                var kitchenIngredients = context.KitchenIngredients
                    .Where(rec => rec.KitchenId == model.Id.Value)
                    .ToList();

                context.KitchenIngredients
                    .RemoveRange(kitchenIngredients
                        .Where(rec => !model.KitchenIngredients
                            .ContainsKey(rec.IngredientId))
                                .ToList());
                context.SaveChanges();

                foreach (var updateIngredient in kitchenIngredients)
                {
                    updateIngredient.Count = model.KitchenIngredients[updateIngredient.IngredientId].Item2;
                    model.KitchenIngredients.Remove(updateIngredient.IngredientId);
                }

                context.SaveChanges();
            }

            foreach (var pc in model.KitchenIngredients)
            {
                context.KitchenIngredients.Add(new KitchenIngredient
                {
                    KitchenId = kitchen.Id,
                    IngredientId = pc.Key,
                    Count = pc.Value.Item2
                });

                context.SaveChanges();
            }

            return kitchen;
        }

        public bool CheckIngredientsCount(int orderID)
        {
            using (var context = new SushiBarDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var SushiId = context.Sushis.FirstOrDefault(rec => rec.Id == context.Orders.FirstOrDefault(order => order.Id == orderID).SushiId).Id;
                        var countOrders = context.Orders.FirstOrDefault(rec => rec.Id == orderID).Count;

                        foreach (var component in context.SushiIngredients.Where(rec => rec.SushiId == SushiId))
                        {
                            int countIngredients = component.Count * countOrders;

                            foreach (var kitchenIngredient in context.KitchenIngredients.Where(rec => rec.IngredientId == component.IngredientId))
                            {
                                if (kitchenIngredient.Count > countIngredients)
                                {
                                    kitchenIngredient.Count -= countIngredients;
                                    countIngredients = 0;
                                    break;
                                }
                                else
                                {
                                    countIngredients -= kitchenIngredient.Count;
                                    kitchenIngredient.Count = 0;
                                }
                            }
                            if (countIngredients > 0)
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }
                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw new Exception("На складе нет необходимых компонентов");
                    }
                }
            }
        }
    }
}
