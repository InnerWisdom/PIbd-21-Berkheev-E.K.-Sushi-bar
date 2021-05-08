using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace SushiBarBusinessLogic.ViewModels
{
    [DataContract]
    public class SushiViewModel
    {
        [DataMember]
        public int Id { get; set; }
        
        [DataMember]
        [DisplayName("Название суши")]
        public string SushiName { get; set; }

        [DataMember]
        [DisplayName("Цена")]
        public decimal Price { get; set; }

        [DataMember]
        public Dictionary<int, (string, int)> SushiIngredients { get; set; }
    }
}
