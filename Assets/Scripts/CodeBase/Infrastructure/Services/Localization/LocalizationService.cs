using System;
using System.Collections.Generic;
using CodeBase.StaticData;
using CodeBase.Utils.Disposables;
using GamePush;
using UnityEngine.Events;

namespace CodeBase.Infrastructure.Services.Localization
{
    public class LocalizationService : ILocalizationService
    {
        private Action _onLocalizationChanged;


        public Dictionary<string, string> CurrentLocalization { get; set; }

        public LocalizationService()
        {
            var language = GP_Language.Current();
            
            switch (language)
            {
                case Language.Russian:
                    CurrentLocalization = DefsFacade.Instance.GetLocalizationDef(LocalizationType.Russian).GetLocale();
                    break;
                case Language.English:
                    CurrentLocalization = DefsFacade.Instance.GetLocalizationDef(LocalizationType.English).GetLocale();
                    break;
                default:
                    CurrentLocalization = DefsFacade.Instance.GetLocalizationDef(LocalizationType.English).GetLocale();
                    break;
            }
        }

        public void ChangeLocalization(LocalizationType localization)
        {
            CurrentLocalization = DefsFacade.Instance.GetLocalizationDef(localization).GetLocale();
            _onLocalizationChanged.Invoke();
        }

        public IDisposable Subscribe(Action call)
        {
            _onLocalizationChanged += call;
            return new ActionDisposables(() => _onLocalizationChanged -= call);
        }
    }
}