using System;
using System.Text;

namespace Experiments.PropertyParameters
{
    /// <summary>
    /// Parameter Name:  ENCODING
    ///
    /// To specify an alternate inline encoding for the property value.
    ///
    /// Format Definition:  This property parameter is defined by the following notation:
    /// encodingparam      = "ENCODING" "="
    /// ( "8BIT"
    /// ; "8bit" text encoding is defined in [RFC2045]
    /// / "BASE64"
    /// ; "BASE64" binary encoding format is defined in [RFC4648]
    /// )
    /// This property parameter identifies the inline encoding used in a property value.  The default encoding is "8BIT", corresponding to a property value
    /// consisting of text. The "BASE64" encoding type corresponds to a property value encoded using the "BASE64" encoding defined in [RFC2045].
    ///
    /// If the value type parameter is ";VALUE=BINARY", then the inline encoding parameter MUST be specified with the value ";ENCODING=BASE64".
    ///
    /// https://tools.ietf.org/html/rfc5545#section-3.2.7
    /// </summary>
    /// <example>
    /// ATTACH;FMTTYPE=text/plain;ENCODING=BASE64;VALUE=BINARY:TG9yZW
    ///  0gaXBzdW0gZG9sb3Igc2l0IGFtZXQsIGNvbnNlY3RldHVyIGFkaXBpc2ljaW
    ///  5nIGVsaXQsIHNlZCBkbyBlaXVzbW9kIHRlbXBvciBpbmNpZGlkdW50IHV0IG
    ///  xhYm9yZSBldCBkb2xvcmUgbWFnbmEgYWxpcXVhLiBVdCBlbmltIGFkIG1pbm
    ///  ltIHZlbmlhbSwgcXVpcyBub3N0cnVkIGV4ZXJjaXRhdGlvbiB1bGxhbWNvIG
    ///  xhYm9yaXMgbmlzaSB1dCBhbGlxdWlwIGV4IGVhIGNvbW1vZG8gY29uc2VxdW
    ///  F0LiBEdWlzIGF1dGUgaXJ1cmUgZG9sb3IgaW4gcmVwcmVoZW5kZXJpdCBpbi
    ///  B2b2x1cHRhdGUgdmVsaXQgZXNzZSBjaWxsdW0gZG9sb3JlIGV1IGZ1Z2lhdC
    ///  BudWxsYSBwYXJpYXR1ci4gRXhjZXB0ZXVyIHNpbnQgb2NjYWVjYXQgY3VwaW
    ///  RhdGF0IG5vbiBwcm9pZGVudCwgc3VudCBpbiBjdWxwYSBxdWkgb2ZmaWNpYS
    ///  BkZXNlcnVudCBtb2xsaXQgYW5pbSBpZCBlc3QgbGFib3J1bS4=
    /// </example>
    /// <remarks>This property is typically optional, except in binary attachments</remarks>
    public readonly struct InlineEncoding :
        IValueType
    {
        public string Name => "ENCODING";
        public string Encoding { get; }
        public string Value { get; }
        public bool IsEmpty => Encoding is null;

        public InlineEncoding(string encoding, string value)
        {
            Encoding = string.IsNullOrWhiteSpace(encoding)
                ? null
                : string.Equals(EightBit, encoding, StringComparison.OrdinalIgnoreCase) || string.Equals(Base64, encoding, StringComparison.OrdinalIgnoreCase)
                    ? encoding
                    : throw new ArgumentException($"{encoding} is not a recognized encoding");

            if (string.Equals(value, ValueBinary, StringComparison.Ordinal)
                && !string.Equals(Encoding, Base64, StringComparison.Ordinal))
            {
                throw new ArgumentException($"When FORMAT={ValueBinary}, the encoding MUST be {Base64}");
            }

            Value = string.IsNullOrWhiteSpace(value)
                ? null
                : value;
        }

        public InlineEncoding(string encoding) :
            this(encoding, value: null) {}

        public override string ToString()
        {
            if (Encoding is null)
            {
                return null;
            }

            var sb = new StringBuilder();
            sb.Append("ENCODING=").Append(Encoding);

            if (Value is object)
            {
                sb.Append(";VALUE=").Append(Value);
            }
            return sb.ToString();
        }

        public static string Base64 => "BASE64";
        public static string Default => EightBit;
        public static string EightBit => "8BIT";
        public static string ValueBinary => "BINARY";
    }
}