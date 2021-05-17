using System;
using SushiBarBusinessLogic.Attributes;
using System.Runtime.Serialization;

namespace SushiBarBusinessLogic.ViewModels
{
    [DataContract]
    public class MessageInfoViewModel
    {
        [DataMember]
        [Column(visible: false)]
        public string MessageId { get; set; }

        [DataMember]
        [Column(title: "Sender", width: 100)]
        public string SenderName { get; set; }

        [DataMember]
        [Column(title: "Delivery date", width: 100)]
        public DateTime DateDelivery { get; set; }

        [Column(title: "Subject", width: 100)]
        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        [Column(title: "Text", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string Body { get; set; }
    }
}
