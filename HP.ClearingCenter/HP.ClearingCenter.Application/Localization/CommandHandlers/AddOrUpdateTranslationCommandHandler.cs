using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.Data.Entities;
using HP.ClearingCenter.Application.Localization.Commands;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Localization.CommandHandlers
{
    public class AddOrUpdateTranslationCommandHandler : ICommandHandler<AddOrUpdateTranslationCommand>
    {
        public void Execute(ICommandContext<AddOrUpdateTranslationCommand> context)
        {
            using (var db = new ClearingCenterDataContext())
            {
                var translation = db.Translators
                    .FirstOrDefault(x =>
                        x.CountryIsoCode == context.Args.CountryIsoCode &&
                        x.LanguageIsoCode == context.Args.LanguageIsoCode &&
                        x.Shortcut == context.Args.ResourceKey);

                if (translation == null)
                {
                    translation = db.Translators.Add(new Translator
                    {
                        CountryIsoCode = context.Args.CountryIsoCode,
                        LanguageIsoCode = context.Args.LanguageIsoCode,
                        Shortcut = context.Args.ResourceKey
                    });
                }

                translation.TextValue = context.Args.TextFormat;

                db.SaveChanges();
            }
        }
    }
}
