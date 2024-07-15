using System;
using System.Drawing;
using System.IO;
using QRCoder;
using ZXing;
using ZXing.Common;


namespace VehicleTrafficManagement.Services
{
    public class QrGenerator
    {
        public static string GenerateQRCode(string chassi)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(chassi, QRCodeGenerator.ECCLevel.Q);
            Base64QRCode qrCode = new Base64QRCode(qrCodeData);

            var imgType = Base64QRCode.ImageType.Png;
            string base64QRcode = qrCode.GetGraphic(20, Color.Black, Color.White, true, imgType);
            return base64QRcode;
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
            // Converte Base64 para imagem
            Image qrImage = Base64ToImage(base64QRCode);

            // Cria o leitor de QR Code
            var barcodeReader = new BarcodeReader();
            var result = barcodeReader.Decode((Bitmap)qrImage);

            return result?.Text; // Retorna o texto decodificado ou null se não conseguir
        }

    }
}
