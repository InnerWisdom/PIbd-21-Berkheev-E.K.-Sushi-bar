﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SushiBarBusinessLogic.BindingModels;
using SushiBarBusinessLogic.Interfaces;
using SushiBarBusinessLogic.ViewModels;
using SushiBarDatabaseImplement.Models;

namespace SushiBarDatabaseImplement.Implements
{
    public class SushiStorage : ISushiStorage
    {
        public List<SushiViewModel> GetFullList()
        {
            using (SushiBarDatabase context = new SushiBarDatabase())
            {
                return context.Sushis
                .Include(rec => rec.SushiIngredients)
                .ThenInclude(rec => rec.Ingredient).ToList()
                .Select(rec => new SushiViewModel
                {
                    Id = rec.Id,
                    SushiName = rec.SushiName,
                    Price = rec.Price,
                    SushiIngredients = rec.SushiIngredients
                .ToDictionary(recCC => recCC.IngredientId, recCC =>
                (recCC.Ingredient?.IngredientName, recCC.Count))
                }).ToList();
            }
        }
        public List<SushiViewModel> GetFilteredList(SushiBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (SushiBarDatabase context = new SushiBarDatabase())
            {
                return context.Sushis
                    .Include(rec => rec.SushiIngredients)
                    .ThenInclude(rec => rec.Ingredient)
                .Where(rec => rec.SushiName.Contains(model.SushiName)).ToList().Select(rec => new SushiViewModel
                {
                    Id = rec.Id,
                    SushiName = rec.SushiName,
                    Price = rec.Price,
                   SushiIngredients = rec.SushiIngredients
                .ToDictionary(recCC => recCC.IngredientId, recCC => (recCC.Ingredient?.IngredientName, recCC.Count))
                }).ToList();
            }
        }
        public SushiViewModel GetElement(SushiBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (SushiBarDatabase context = new SushiBarDatabase())
            {
                Sushi sushi = context.Sushis
                    .Include(rec => rec.SushiIngredients)
                    .ThenInclude(rec => rec.Ingredient)
                .FirstOrDefault(rec => rec.SushiName == model.SushiName || rec.Id == model.Id);
                return sushi != null ? new SushiViewModel
                {
                    Id = sushi.Id,
                    SushiName = sushi.SushiName,
                    Price = sushi.Price,
                    SushiIngredients = sushi.SushiIngredients.ToDictionary(recPC => recPC.IngredientId, 
                    recPC => (recPC.Ingredient?.IngredientName, recPC.Count))
                } : null;
            }
        }
        public void Insert(SushiBindingModel model)
        {
            using (SushiBarDatabase context = new SushiBarDatabase())
            {
                using (Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var r = CreateModel(model, new Sushi());
                        context.Sushis.Add(r);
                        context.SaveChanges();
                        CreateModel(model, r, context);
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
        public void Update(SushiBindingModel model)
        {
            using (SushiBarDatabase context = new SushiBarDatabase())
            {
                using (Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Sushi element = context.Sushis.FirstOrDefault(rec => rec.Id == model.Id);
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
        public void Delete(SushiBindingModel model)
        {
            using (SushiBarDatabase context = new SushiBarDatabase())
            {
                Sushi element = context.Sushis.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Sushis.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        private Sushi CreateModel(SushiBindingModel model, Sushi sushi)
        {
            sushi.SushiName = model.SushiName;
            sushi.Price = model.Price;
            return sushi;
        }

        private Sushi CreateModel(SushiBindingModel model, Sushi sushi, SushiBarDatabase context)
        {
            sushi.SushiName = model.SushiName;
            sushi.Price = model.Price;
            if (model.Id.HasValue)
            {
                var sushiIngredients = context.SushiIngredients.Where(rec => rec.SushiId == model.Id.Value).ToList();
                context.SushiIngredients.RemoveRange(sushiIngredients.Where(rec => !model.SushiIngredients.ContainsKey(rec.IngredientId)).ToList());
                context.SaveChanges();
                foreach (var updateIngredient in sushiIngredients)
                {
                    updateIngredient.Count = model.SushiIngredients[updateIngredient.IngredientId].Item2;
                    model.SushiIngredients.Remove(updateIngredient.IngredientId);
                }
                context.SaveChanges();
            }
            foreach (var pc in model.SushiIngredients)
            {
                context.SushiIngredients.Add(new SushiIngredient
                {
                    SushiId = sushi.Id,
                    IngredientId = pc.Key,
                    Count = pc.Value.Item2
                });

            }
            context.SaveChanges();
            return sushi;
        }
    }
}
