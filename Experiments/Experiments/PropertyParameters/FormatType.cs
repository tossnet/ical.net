using System;
using System.Text.RegularExpressions;
using Experiments.Utilities;

namespace Experiments.PropertyParameters
{
    /// <summary>
    /// Parameter Name:  FMTTYPE
    ///
    /// To specify the content type of a referenced object.
    ///
    /// Format Definition:  This property parameter is defined by the following notation:
    ///
    /// fmttypeparam = "FMTTYPE" "=" type-name "/" subtype-name
    ///  ; Where "type-name" and "subtype-name" are
    ///  ; defined in Section 4.2 of [RFC4288].
    ///
    /// This parameter can be specified on properties that are used to reference an object.  The parameter specifies the media type [RFC4288] of the referenced
    /// object.  For example, on the "ATTACH" property, an FTP type URI value does not, by itself, necessarily convey the type of content associated with the
    /// resource.  The parameter value MUST be the text for either an IANA-registered media type or a non-standard media type.
    ///
    /// https://tools.ietf.org/html/rfc5545#section-3.2.8
    /// </summary>
    /// <example>
    /// ATTACH;FMTTYPE=application/msword:ftp://example.com/pub/docs/
    ///  agenda.doc
    /// </example>
    public readonly struct FormatType :
        IValueType
    {
        public string Name => "FMTTYPE";
        public string Value { get; }
        public bool IsEmpty => Value is null;

        private static readonly Regex _validation = new Regex(
            pattern: "^(application|audio|font|example|image|message|model|multipart|text|video|x-(?:[0-9A-Za-z!#$%&'*+.^_`|~-]+))/([0-9A-Za-z!#$%&'*+.^_`|~-]+)((?:[ \t]*;[ \t]*[0-9A-Za-z!#$%&'*+.^_`|~-]+=(?:[0-9A-Za-z!#$%&'*+.^_`|~-]+|\"(?:[^\"\\\\]|\\.)*\"))*)$",
            options: RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public FormatType(string mediaType)
        {
            if (string.IsNullOrWhiteSpace(mediaType))
            {
                Value = null;
                return;
            }

            Value = _validation.IsMatch(mediaType)
                ? mediaType
                : throw new ArgumentException($"{mediaType} is not a valid media type");
        }

        public override string ToString() => this.NameEqualsValue();
    }
}