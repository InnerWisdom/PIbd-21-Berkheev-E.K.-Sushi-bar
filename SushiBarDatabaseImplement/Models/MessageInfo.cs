using System;
using System.ComponentModel.DataAnnotations;

namespace SushiBarDatabaseImplement.Models
{
    /// <summary>
    /// Сообщения, приходящие на почту
    /// </summary>
    public class MessageInfo
    {
        [Key]
        public string MessageId { get; set; }
        public int? ClientId { get; set; }
        public string SenderName { get; set; }
        public DateTime DateDelivery { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public virtual Client Client { get; set; }
    }
}
