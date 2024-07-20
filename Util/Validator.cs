using VehicleTrafficManagement.Interfaces;

namespace VehicleTrafficManagement.Util
{
    public class Validator
    {
        public static bool IsValidTaxNumber(string taxNumber)
        {
            taxNumber = Formatter.RemoveMaskTaxNumber(taxNumber);

            if (taxNumber.Length == 11)
            {
                return IsValidCPF(taxNumber);
            }
            else if (taxNumber.Length == 14)
            {
                return IsValidCNPJ(taxNumber);
            }

            return false;
        }

        private static bool IsValidCPF(string cpf)
        {
            if (cpf.Length != 11)
                return false;

            int[] multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int sum = 0;

            for (int i = 0; i < 9; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];

            int remainder = sum % 11;
            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;

            string digit = remainder.ToString();
            tempCpf += digit;

            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];

            remainder = sum % 11;
            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;

            digit += remainder.ToString();

            return cpf.EndsWith(digit);
        }

        private static bool IsValidCNPJ(string cnpj)
        {
            if (cnpj.Length != 14)
                return false;

            int[] multiplier1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj.Substring(0, 12);
            int sum = 0;

            for (int i = 0; i < 12; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multiplier1[i];

            int remainder = sum % 11;
            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;

            string digit = remainder.ToString();
            tempCnpj += digit;

            sum = 0;
            for (int i = 0; i < 13; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multiplier2[i];

            remainder = sum % 11;
            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;

            digit += remainder.ToString();

            return cnpj.EndsWith(digit);
        }


        public static bool IsPasswordValid(string password)
        {
            {
                return !string.IsNullOrEmpty(password) && password.Length >= 6;
            }
        }

        public static bool IsChassisValid(string chassis)
        {
            if (string.IsNullOrEmpty(chassis) || chassis.Length != 17)
                return false;

            return true;
        }
    }
}
