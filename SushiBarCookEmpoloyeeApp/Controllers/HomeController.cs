using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SushiBarBusinessLogic.ViewModels;
using SushiBarBusinessLogic.BindingModels;
using SushiBarCookEmployeeApp.Models;

namespace SushiBarCookEmployeeApp.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            if (Program.Enter == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(ApiCookEmployee.GetRequest<List<KitchenViewModel>>("api/kitchen/GetKitchenList"));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Enter()
        {
            return View();
        }

        [HttpPost]
        public void Enter(string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                if (password != Program.CurrentPassword)
                {
                    throw new Exception("Invalid password");
                }
                Program.Enter = true;
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Enter Password");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public void Create(string name, string responsiblePersonFullName)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(responsiblePersonFullName))
            {
                ApiCookEmployee.PostRequest("api/kitchen/CreateOrUpdateKitchen", new KitchenBindingModel
                {
                    ResponsiblePersonFullName = responsiblePersonFullName,
                    KitchenName = name,
                    DateCreate = DateTime.Now,
                    KitchenIngredients = new Dictionary<int, (string, int)>()
                });
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Type in responsible person full name");
        }

        [HttpGet]
        public IActionResult Update(int kitchenId)
        {
            var kitchen = ApiCookEmployee.GetRequest<KitchenViewModel>($"api/kitchen/GetKitchen?kitchenId={kitchenId}");
            ViewBag.Ingredients = kitchen.KitchenIngredients.Values;
            ViewBag.Name = kitchen.KitchenName;
            ViewBag.ResponsiblePersonFullName = kitchen.ResponsiblePersonFullName;
            return View();
        }

        [HttpPost]
        public void Update(int kitchenId, string name, string responsiblePersonFullName)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(responsiblePersonFullName))
            {
                var kitchen = ApiCookEmployee.GetRequest<KitchenViewModel>($"api/kitchen/GetKitchen?kitchenId={kitchenId}");
                if (kitchen == null)
                {
                    return;
                }
                ApiCookEmployee.PostRequest("api/kitchen/CreateOrUpdateKitchen", new KitchenBindingModel
                {
                    ResponsiblePersonFullName = responsiblePersonFullName,
                    KitchenName = name,
                    DateCreate = DateTime.Now,
                    KitchenIngredients = kitchen.KitchenIngredients,
                    Id = kitchen.Id
                });
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Enter login, password and full name");
        }

        [HttpGet]
        public IActionResult Delete()
        {
            if (Program.Enter == null)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.Kitchen = ApiCookEmployee.GetRequest<List<KitchenViewModel>>("api/kitchen/GetKitchenList");
            return View();
        }

        [HttpPost]
        public void Delete(int kitchenId)
        {
            ApiCookEmployee.PostRequest("api/kitchen/DeleteKitchen", new KitchenBindingModel
            {
                Id = kitchenId
            });
            Response.Redirect("Index");
        }

        [HttpGet]
        public IActionResult AddIngredientsToKitchen()
        {
            if (Program.Enter == null)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.Kitchen = ApiCookEmployee.GetRequest<List<KitchenViewModel>>("api/kitchen/GetKitchenList");
            ViewBag.Ingredient = ApiCookEmployee.GetRequest<List<IngredientViewModel>>("api/kitchen/GetIngredientList");
            return View();
        }

        [HttpPost]
        public void AddIngredientsToKitchen(int kitchenId, int ingredientId, int count)
        {
            ApiCookEmployee.PostRequest("api/kitchen/Refill", new KitchenFillBindingModel
            {
                KitchenId = kitchenId,
                IngredientId = ingredientId,
                Count = count
            });
            Response.Redirect("AddIngredientsToKitchen");
        }
    }
}
