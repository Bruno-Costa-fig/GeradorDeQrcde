using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace GeradorDeQrcde.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeradorQRCodeController : ControllerBase
    {

        public GeradorQRCodeController()
        {
        }

        [HttpGet("qrcode")]
        public async Task<FileContentResult> GerarQrcode(string url)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new PngByteQRCode(qrCodeData);
            var stringQrCode = qrCode.GetGraphic(20, true);
            Image qrCodeImage;
            var CurrentDirectory = Directory.GetCurrentDirectory();
            Console.WriteLine(CurrentDirectory);

            using (var ms = new MemoryStream(stringQrCode))
            {
                qrCodeImage = Image.FromStream(ms);
                qrCodeImage.Save(ms, ImageFormat.Png);
                //grSave.Save();
                //ms.ToArray();
                //return File(ms, "image/jpeg");
                return File(ms.ToArray(), "image/jpeg", "QRCode.jpeg");
            }
        }
    }
}