using System;

namespace Experiments.PropertyParameters
{
    /// <summary>
    /// Parameter Name:  DIR
    ///
    /// To specify reference to a directory entry associated with the calendar user specified by the property.
    ///
    /// Format Definition:  This property parameter is defined by the following notation:
    /// dirparam   = "DIR" "=" DQUOTE uri DQUOTE
    ///
    /// This parameter can be specified on properties with a CAL-ADDRESS value type.  The parameter specifies a reference to the directory entry associated with
    /// the calendar user specified by the property.  The parameter value is a URI.  The URI parameter value MUST be specified in a quoted-string.
    ///
    /// Note: While there is no restriction imposed on the URI schemes allowed for this parameter, CID [RFC2392], DATA [RFC2397], FILE [RFC1738], FTP [RFC1738],
    /// HTTP [RFC2616], HTTPS [RFC2818], LDAP [RFC4516], and MID [RFC2392] are the URI schemes most commonly used by current implementations.
    ///
    /// https://tools.ietf.org/html/rfc5545#section-3.2.6
    /// </summary>
    /// <example>
    /// ORGANIZER;DIR="ldap://example.com:6666/o=ABC%20Industries,
    ///  c=US???(cn=Jim%20Dolittle)":mailto:jimdo@example.com
    /// </example>
    public readonly struct DirectoryEntryReference :
        IValueType
    {
        public string Name => "DIR";
        public Uri Uri { get; }
        public string Value => Uri?.OriginalString;
        public bool IsEmpty => Uri is null;

        public DirectoryEntryReference(string directoryUri)
        {
            if (string.IsNullOrWhiteSpace(directoryUri)) throw new ArgumentNullException(nameof(directoryUri));
            Uri = new Uri(directoryUri, UriKind.RelativeOrAbsolute);
        }

        public DirectoryEntryReference(Uri directoryUri)
        {
            Uri = directoryUri;
        }

        public override string ToString()
        {
            return Uri is null
                ? null
                : $"{Name}=\"{Value}\"";
        }
    }
}