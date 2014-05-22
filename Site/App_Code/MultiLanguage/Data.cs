using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Holds data for contstructing a Language Switcher
/// </summary>
namespace Goyaweb.MultiLanguage
{
    public class Data
    {
        public Data()
        {
        }
        public string NativeName { get; set; }
        public string LanguageCode { get; set; }
        public string CountryCode { get; set; }
        public string Url { get; set; }
    }
}