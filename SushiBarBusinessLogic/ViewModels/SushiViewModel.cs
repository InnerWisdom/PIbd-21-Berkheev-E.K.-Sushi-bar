
using SushiBarBusinessLogic.Attributes;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace SushiBarBusinessLogic.ViewModels
{
    [DataContract]
    public class SushiViewModel
    {

        [Column(title: "Number", width: 100)]
        [DataMember]
        public int Id { get; set; }
        
        [DataMember]
        [Column(title: "Sushi name", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string SushiName { get; set; }

        [DataMember]
        [Column(title: "Price", width: 100)]
        public decimal Price { get; set; }

        [DataMember]
        [Column(visible: false)]
        public Dictionary<int, (string, int)> SushiIngredients { get; set; }
    }
}
