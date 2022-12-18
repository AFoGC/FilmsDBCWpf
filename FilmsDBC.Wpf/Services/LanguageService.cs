using System;
using System.Collections.Generic;
using System.Globalization;

namespace WpfApp.Services
{
    public class LanguageService
    {
        public event Action<CultureInfo> LanguageChanged;

        private readonly List<CultureInfo> _languages;

        public IEnumerable<CultureInfo> Languages => _languages;
        public CultureInfo CurrentLanguage { get; private set; }

        public LanguageService()
        {
            CurrentLanguage = CultureInfo.GetCultureInfo("en");

            _languages = new List<CultureInfo>();

            _languages.Add(CurrentLanguage);
            _languages.Add(CultureInfo.GetCultureInfo("ru"));
            _languages.Add(CultureInfo.GetCultureInfo("uk-UA"));
        }

        public bool SetLanguage(CultureInfo lang)
        {
            bool hasLanguage = _languages.Contains(lang);

            if (hasLanguage)
            {
                CurrentLanguage = lang;
                LanguageChanged?.Invoke(lang);
            }

            return hasLanguage;
        }

        public bool SetLanguage(string name)
        {
            var culture = CultureInfo.GetCultureInfo(name);
            return SetLanguage(culture);
        }
    }
}
