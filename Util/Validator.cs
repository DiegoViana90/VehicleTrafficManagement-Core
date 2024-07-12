using VehicleTrafficManagement.Interfaces;

namespace VehicleTrafficManagement.Util
{
    public static class Validator
    {
        public static bool IsCNPJ(string CNPJ)
        {
            int[] multiplier1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int sum;
            int remainder;
            string digit;
            string tempCnpj;
            CNPJ = CNPJ.Trim();
            CNPJ = CNPJ.Replace(".", "").Replace("-", "").Replace("/", "");
            if (CNPJ.Length != 14)
                return false;
            tempCnpj = CNPJ.Substring(0, 12);
            sum = 0;
            for (int i = 0; i < 12; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multiplier1[i];
            remainder = (sum % 11);
            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;
            digit = remainder.ToString();
            tempCnpj = tempCnpj + digit;
            sum = 0;
            for (int i = 0; i < 13; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multiplier2[i];
            remainder = (sum % 11);
            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;
            digit = digit + remainder.ToString();
            return CNPJ.EndsWith(digit);
        }
        public static bool IsPasswordValid(string password)
        {
            {
                return !string.IsNullOrEmpty(password) && password.Length >= 6;
            }
        }
    }
}
