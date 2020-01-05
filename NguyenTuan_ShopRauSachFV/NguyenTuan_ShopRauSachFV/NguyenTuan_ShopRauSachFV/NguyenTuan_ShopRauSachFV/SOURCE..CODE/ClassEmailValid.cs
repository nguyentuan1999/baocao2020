using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Globalization;
using System.Text.RegularExpressions;

namespace RAU_SACH_THANH_TRUC
{
    public class ClassEmailValid
    {
        bool inValid = false;

        public bool IsValid_Email(string Email_Address)
        {
            inValid = false;

            if (String.IsNullOrEmpty(Email_Address)) { return false; }

            // USE IDNMAPPING CLASS TO CONVERT UNICODE DOMAIN NAMES

            Email_Address = Regex.Replace(Email_Address, @"(@)(.+)$", this.DomainMapper);

            if (inValid) { return false; }

            // RETURN TRUE IF STRIN IS IN VALID E-MAIL FORMAT

            return Regex.IsMatch(Email_Address,
                   @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                   @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
                   RegexOptions.IgnoreCase);
        }

        private string DomainMapper(Match match)
        {
            // IDNMAPPING CLASS WITH DEFAULT PROPERTY VALUES

            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                inValid = true;
            }
            return match.Groups[1].Value + domainName;
        }
    }
}