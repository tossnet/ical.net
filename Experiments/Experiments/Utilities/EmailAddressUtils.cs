using System;
using System.Net.Mail;

namespace Experiments.Utilities
{
    public static class EmailAddressUtils
    {
        /// <summary>
        /// Parses email addresses, and returns just the address. Supported examples include: name@example.com, mailto:name@example.com, name@example.com <Name>
        /// </summary>
        /// <param name="email"></param>
        /// <returns>The email address prefixed with the mailto: URI scheme</returns>
        public static string ParseAndExtractEmailAddress(this string email)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException(nameof(email));
            const string mailtoPrefix = "mailto:";

            var normalized = email;
            if (normalized.StartsWith(mailtoPrefix, StringComparison.OrdinalIgnoreCase))
            {
                normalized = email.Substring(mailtoPrefix.Length);
            }
                    
            var addr = new MailAddress(normalized);
            return addr.Address;
        }
    }
}