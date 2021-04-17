using Experiments.Utilities;

namespace Experiments.PropertyParameters
{
    /// <summary>
    /// Parameter Name:  LANGUAGE
    ///
    /// To specify the language for text values in a property or property parameter.
    ///
    /// Format Definition:  This property parameter is defined by the following notation:
    ///     languageparam = "LANGUAGE" "=" language
    ///     language = Language-Tag
    ///     ; As defined in [RFC5646].
    ///
    /// This parameter identifies the language of the text in the property value and of all property parameter values of the property.  The value of the
    /// "LANGUAGE" property parameter is that defined in [RFC5646].
    ///
    /// For transport in a MIME entity, the Content-Language header field can be used to set the default language for the entire body part. Otherwise, no
    /// default language is assumed.
    ///
    /// https://tools.ietf.org/html/rfc5545#section-3.2.10
    /// </summary>
    /// <example>
    /// The following are examples of this parameter on the "SUMMARY" and "LOCATION" properties:
    /// SUMMARY;LANGUAGE=en-US:Company Holiday Party
    /// LOCATION;LANGUAGE=en:Germany
    /// LOCATION;LANGUAGE=no:Tyskland
    /// </example>
    public readonly struct Language : 
        IValueType
    {
        public string Name => "LANGUAGE";
        public string Value { get; }
        public bool IsEmpty => Value is null;

        public Language(string language)
        {
            Value = string.IsNullOrWhiteSpace(language)
                ? null
                : CultureExtensions.GetNormalizedLanguageTag(language);
        }

        public override string ToString() => ValueTypeUtilities.GetToString(this);
    }
}