using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.Services.Localization
{
    public interface ILocalizationService : IService
    {
        Dictionary<string, string> CurrentLocalization { get; set; }
        void ChangeLocalization(LocalizationType localization);
        IDisposable Subscribe(Action call);
    }
}