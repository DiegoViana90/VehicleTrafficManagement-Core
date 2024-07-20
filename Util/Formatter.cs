using System;
using System.Text.RegularExpressions;

namespace VehicleTrafficManagement.Util
{
    public class Formatter
    {
        public static string RemoveMaskTaxNumber(string taxNumber)
        {
            return Regex.Replace(taxNumber, @"[^\d]", "");
        }
    }
}
