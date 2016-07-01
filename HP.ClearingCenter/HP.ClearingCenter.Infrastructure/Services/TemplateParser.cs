using RazorEngine.Templating;

namespace HP.ClearingCenter.Infrastructure.Services
{
    public class TemplateParser 
    {
        public static string Compile<TParameter>(string template, TParameter parameter) where TParameter : class
        {
            var templateService = new TemplateService();
            return templateService.Parse(template, parameter, null, null);
        }
    }
}
