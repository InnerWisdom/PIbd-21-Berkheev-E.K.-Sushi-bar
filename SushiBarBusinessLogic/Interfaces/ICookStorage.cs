using System.Collections.Generic;
using SushiBarBusinessLogic.BindingModels;
using SushiBarBusinessLogic.ViewModels;

namespace SushiBarBusinessLogic.Interfaces
{
    public interface ICookStorage
    {
        List<CookViewModel> GetFullList();
        List<CookViewModel> GetFilteredList(CookBindingModel model);
        CookViewModel GetElement(CookBindingModel model);
        void Insert(CookBindingModel model);
        void Update(CookBindingModel model);
        void Delete(CookBindingModel model);
    }
}
