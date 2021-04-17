using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Experiments.Utilities
{
    public static class CultureExtensions
    {
        private static readonly Dictionary<string, CultureInfo> _byIetfLanguageTag = CultureInfo.GetCultures(CultureTypes.AllCultures)
            .ToDictionary(ci => ci.IetfLanguageTag, StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Returns the case-normalized IETF language tag.
        /// </summary>
        /// <param name="languageTag"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Thrown when an unknown IETF language tag is supplied</exception>
        public static string GetNormalizedLanguageTag(string languageTag)
        {
            if (string.IsNullOrWhiteSpace(languageTag))
            {
                throw new ArgumentNullException(nameof(languageTag));
            }
            
            if (_byIetfLanguageTag.TryGetValue(languageTag, out var ci))
            {
                return ci.IetfLanguageTag;
            }

            throw new ArgumentException($"{languageTag} is not a known IETF language tag");
        }
    }
}