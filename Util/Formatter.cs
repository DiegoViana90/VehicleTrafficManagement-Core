using System;

namespace VehicleTrafficManagement.Util
{
    public class Formatter
    {
        public static string RemoveMaskCnpj(string CNPJ)
        {
            return CNPJ.Replace("/", "").Replace(".", "").Replace("-", "");
        }
    }
}
