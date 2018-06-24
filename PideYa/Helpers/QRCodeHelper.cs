using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;

namespace PideYa.Helpers
{
    public class QRCodeHelper
    {
        public Image GenerateQRCode(string content, int size)
        {
            var encoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode;
            encoder.TryEncode(content, out qrCode);

            var gRenderer = new GraphicsRenderer(new FixedModuleSize(4, QuietZoneModules.Two), Brushes.Black, Brushes.White);
            //Graphics g = gRenderer.Draw(qrCode.Matrix);

            var ms = new MemoryStream();
            gRenderer.WriteToStream(qrCode.Matrix, ImageFormat.Bmp, ms);

            var imageTemp = new Bitmap(ms);

            var image = new Bitmap(imageTemp, new Size(new Point(size, size)));

            //image.Save("file.bmp", ImageFormat.Bmp);

            return image;
        }
    }
}