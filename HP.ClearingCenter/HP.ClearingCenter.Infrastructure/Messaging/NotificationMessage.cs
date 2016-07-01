namespace HP.ClearingCenter.Infrastructure.Messaging
{
    public class NotificationMessage
    {
        public NotificationMessage()
        {
            Cc = string.Empty;
            Bcc = string.Empty;
            Attachments = new NotificationAttachment[0];
        }

        public string Recipients { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string Sender { get; set; }

        public string Cc { get; set; }

        public string Bcc { get; set; }

        public NotificationAttachment[] Attachments { get; set; }
    }
}
