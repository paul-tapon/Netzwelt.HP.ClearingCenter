using HP.ClearingCenter.Infrastructure.Configuration;
using HP.ClearingCenter.Infrastructure.Messaging;
using HP.ClearingCenter.Infrastructure.Services;

namespace HP.ClearingCenter.Infrastructure.Helpers
{
    public static class MailHelper
    {
        public static NotificationMessage ToNotificationMessage<TParameter>(this EmailTemplate template,
            TParameter parameter) where TParameter : class
        {
            var result = new NotificationMessage();
            result.Sender = template.Sender;
            result.Cc = template.Cc;
            result.Bcc = template.Bcc;
            result.Recipients = EnvironmentConfiguration.Current.DefaultEmailRecipient;
            result.Subject = EnvironmentConfiguration.Current.MailSubjectPrefix + TemplateParser.Compile(template.Subject, parameter);
            result.Body = TemplateParser.Compile(template.Body, parameter);
            return result;
        }
    }
}
