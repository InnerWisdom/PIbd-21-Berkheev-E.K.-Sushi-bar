using System;
using System.Runtime.Serialization;

namespace SushiBarBusinessLogic.BindingModels
{
    [DataContract]
    public class MessageInfoBindingModel
    {
        /// <summary>
        /// Сообщения, приходящие на почту
        /// </summary>
        [DataMember]
        public int? ClientId { get; set; }
        [DataMember]
        public string MessageId { get; set; }
        [DataMember]
        public string FromMailAddress { get; set; }
        [DataMember]
        public string Subject { get; set; }
        [DataMember]
        public string Body { get; set; }
        [DataMember]
        public DateTime DateDelivery { get; set; }
    }
}
