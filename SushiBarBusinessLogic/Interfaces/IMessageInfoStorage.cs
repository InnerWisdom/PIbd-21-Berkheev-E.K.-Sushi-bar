using SushiBarBusinessLogic.BindingModels;
using SushiBarBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace SushiBarBusinessLogic.Interfaces
{
    public interface IMessageInfoStorage
    {
        List<MessageInfoViewModel> GetFullList();
        List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model);
        void Insert(MessageInfoBindingModel model);
    }
}
