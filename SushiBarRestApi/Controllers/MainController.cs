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
    public class MainController : ControllerBase
    {
        private readonly OrderLogic _order;

        private readonly SushiLogic _sushi;

        private readonly OrderLogic _main;

        public MainController(OrderLogic order, SushiLogic sushi, OrderLogic main)
        {
            _order = order;
            _sushi = sushi;
            _main = main;
        }

        [HttpGet]
        public List<SushiViewModel> GetSushiList() => _sushi.Read(null)?.ToList();

        [HttpGet]
        public SushiViewModel GetSushi(int sushiId) => _sushi.Read(new SushiBindingModel { Id = sushiId })?[0];

        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new OrderBindingModel { ClientId = clientId });

        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) => _main.CreateOrder(model);
    }
}