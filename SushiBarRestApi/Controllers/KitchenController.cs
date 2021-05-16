using SushiBarBusinessLogic.BindingModels;
using SushiBarBusinessLogic.BusinessLogics;
using SushiBarBusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace SushiBarRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class KitchenController : Controller
    {
        private readonly KitchenLogic _kitchen;

        private readonly IngredientLogic _ingredient;

        public KitchenController(KitchenLogic kitchenLogic, IngredientLogic ingredientLogic)
        {
            _kitchen = kitchenLogic;
            _ingredient = ingredientLogic;
        }

        [HttpGet]
        public List<KitchenViewModel> GetKitchenList() => _kitchen.Read(null)?.ToList();

        [HttpPost]
        public void CreateOrUpdateKitchen(KitchenBindingModel model) => _kitchen.CreateOrUpdate(model);

        [HttpPost]
        public void DeleteKitchen(KitchenBindingModel model) => _kitchen.Delete(model);

        [HttpPost]
        public void Refill(KitchenFillBindingModel model) => _kitchen.Refill(model);

        [HttpGet]
        public KitchenViewModel GetKitchen(int kitchenId) => _kitchen.Read(new KitchenBindingModel { Id = kitchenId })?[0];

        [HttpGet]
        public List<IngredientViewModel> GetIngredientList() => _ingredient.Read(null);
    }
}
