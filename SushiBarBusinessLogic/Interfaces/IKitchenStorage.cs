using System;
using System.Collections.Generic;
using System.Text;
using SushiBarBusinessLogic.ViewModels;
using SushiBarBusinessLogic.BindingModels;

namespace SushiBarBusinessLogic.Interfaces
{
    public interface IKitchenStorage
    {
        List<KitchenViewModel> GetFullList();

        List<KitchenViewModel> GetFilteredList(KitchenBindingModel model);

        KitchenViewModel GetElement(KitchenBindingModel model);

        void Insert(KitchenBindingModel model);

        void Update(KitchenBindingModel model);

        void Delete(KitchenBindingModel model);
    }
}
