using ETicaretAPI.Application.Abstractions.Services;
using QRCoder;

namespace ETicaretAPI.Infrastructure.Services
{
    public class QRCodeService : IQRCodeService
    {
        public byte[] GenerateQRCode(string text)
        {
            QRCodeGenerator generator = new();
            QRCodeData data = generator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new(data);
            byte[] byteGraphic = qrCode.GetGraphic(10, new byte[] { 0, 51, 102 }, new byte[] { 255, 255, 255 });
            return byteGraphic;
        }
    }
}
