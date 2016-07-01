using System.IO;

namespace HP.ClearingCenter.Infrastructure.Messaging
{
    public class NotificationAttachment
    {
        public string Filename { get; set; }
        public string ContentType { get; set; }
        public Stream FileStream { get; set; }
    }
}
