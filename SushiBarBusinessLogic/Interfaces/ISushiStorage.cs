using SushiBarBusinessLogic.BindingModels;
using SushiBarBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace SushiBarBusinessLogic.Interfaces
{
    public interface ISushiStorage
    {
        List<SushiViewModel> GetFullList();
        
        List<SushiViewModel> GetFilteredList(SushiBindingModel model);
        
        SushiViewModel GetElement(SushiBindingModel model);
        
        void Insert(SushiBindingModel model);
        
        void Update(SushiBindingModel model);
        
        void Delete(SushiBindingModel model);
    }
}
