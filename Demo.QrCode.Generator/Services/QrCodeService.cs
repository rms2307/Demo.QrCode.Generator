using QRCoder;
using SixLabors.ImageSharp;

namespace Demo.QrCode.Generator.Services
{
    public class QrCodeService : IQrCodeService
    {
        public async Task<byte[]> GerarQrCodeAsync(GerarQrCodeRequest request)
        {
            const int pixelsPerModule = 25;
            try
            {
                QRCodeData qrCodeData;
                using (QRCodeGenerator qrGenerator = new())
                {
                    qrCodeData = qrGenerator.CreateQrCode(request.Url, QRCodeGenerator.ECCLevel.Q);
                }

                var qrCode = new Base64QRCode(qrCodeData);
                string qrCodeImageAsBase64 = qrCode.GetGraphic(pixelsPerModule, Color.Black, Color.White);

                byte[] qrCodeBytes = Convert.FromBase64String(qrCodeImageAsBase64);

                await Task.Delay(600);

                return qrCodeBytes;
            }
            catch
            {
                throw;
            }
        }
    }
}
