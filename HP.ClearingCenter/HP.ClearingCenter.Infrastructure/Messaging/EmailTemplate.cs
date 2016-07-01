using HP.ClearingCenter.Infrastructure.Contracts;

namespace HP.ClearingCenter.Infrastructure.Messaging
{
    public class EmailTemplate
    {
        public string LanguageIsoCode { get; set; }

        public string TemplateCode { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string Sender { get; set; }

        public string Recipients { get; set; }

        public string Cc { get; set; }

        public string Bcc { get; set; }
    }
}
