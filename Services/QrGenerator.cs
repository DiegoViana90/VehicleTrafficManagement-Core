using System;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using QRCoder;
using ZXing;

namespace VehicleTrafficManagement.Services
{
    public class QrGenerator
    {
        private static readonly string Salt = "PTdO9JqiW53SueBXZhWX0SD9Onc7EIshioLrS4N9"; 

        public static string GenerateQRCode(string hashedChassi)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(hashedChassi, QRCodeGenerator.ECCLevel.Q);
            Base64QRCode qrCode = new Base64QRCode(qrCodeData);

            var imgType = Base64QRCode.ImageType.Png;
            string base64QRcode = qrCode.GetGraphic(20, Color.Black, Color.White, true, imgType);
            return base64QRcode;
        }

        public static string ApplySaltAndHash(string chassi)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                string saltedChassi = chassi + Salt;
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(saltedChassi));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString(""));
                }
                return builder.ToString();
            }
        }

        public static Image Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            using (var ms = new MemoryStream(imageBytes))
            {
                return Image.FromStream(ms);
            }
        }

        public static string DecodeQRCode(string base64QRCode)
        {
            Image qrImage = Base64ToImage(base64QRCode);

            var barcodeReader = new BarcodeReader();
            var result = barcodeReader.Decode((Bitmap)qrImage);

            return result?.Text;
        }
    }
}
