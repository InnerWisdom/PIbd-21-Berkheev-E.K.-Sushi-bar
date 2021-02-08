using SushiBarBusinessLogic.BindingModels;
using SushiBarBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace SushiBarBusinessLogic.Interfaces
{
    public interface IIngredientStorage
    {
        List<IngredientViewModel> GetFullList();

        List<IngredientViewModel> GetFilteredList(IngredientBindingModel model);
        
        IngredientViewModel GetElement(IngredientBindingModel model);
        
        void Insert(IngredientBindingModel model);
        
        void Update(IngredientBindingModel model);
        
        void Delete(IngredientBindingModel model);
    }
}
