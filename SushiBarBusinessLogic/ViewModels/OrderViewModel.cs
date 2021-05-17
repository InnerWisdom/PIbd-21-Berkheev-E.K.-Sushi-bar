using SushiBarBusinessLogic.Enums;
using System;
using SushiBarBusinessLogic.Attributes;

using System.Runtime.Serialization;

namespace SushiBarBusinessLogic.ViewModels
{
    [DataContract]
    public class OrderViewModel
    {
        [DataMember]
        [Column(title: "Number", gridViewAutoSize: GridViewAutoSize.Fill)]
        public int Id { get; set; }
        [DataMember]
        public int? CookId { get; set; }
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        public int SushiId { get; set; }
        [DataMember]
        [Column(title: "Cook", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string CookFIO { get; set; }
        [DataMember]
        [Column(title: "Client", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ClientFIO { get; set; }
        [DataMember]
        [Column(title: "Sushi", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string SushiName { get; set; }
        [DataMember]
        [Column(title: "Quantity", gridViewAutoSize: GridViewAutoSize.Fill)]
        public int Count { get; set; }
        [DataMember]
        [Column(title: "Sum", gridViewAutoSize: GridViewAutoSize.Fill)]
        public decimal Sum { get; set; }
        [DataMember]
        [Column(title: "Status", gridViewAutoSize: GridViewAutoSize.Fill)]
        public OrderStatus Status { get; set; }
        [DataMember]
        [Column(title: "Creation date", gridViewAutoSize: GridViewAutoSize.Fill)]
        public DateTime DateCreate { get; set; }
        [DataMember]
        [Column(title: "Implementatation date", gridViewAutoSize: GridViewAutoSize.Fill)]
        public DateTime? DateImplement { get; set; }
    }
}
