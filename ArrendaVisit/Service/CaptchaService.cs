using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace ArrendaVisit.Service
{
    public class CaptchaService
    {
        private static readonly Random Random = new Random();

        public byte[] GenerateCaptcha(out string captchaText)
        {
            captchaText = GenerateRandomText(6);

            using (var bitmap = new Bitmap(200, 50))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(Color.White);
                    using (var font = new Font("Arial", 20, FontStyle.Bold))
                    {
                        graphics.DrawString(captchaText, font, Brushes.Black, new PointF(10, 10));
                    }

                    using (var memoryStream = new MemoryStream())
                    {
                        bitmap.Save(memoryStream, ImageFormat.Png);
                        return memoryStream.ToArray();
                    }
                }
            }
        }

        private string GenerateRandomText(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var randomString = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                randomString.Append(chars[Random.Next(chars.Length)]);
            }
            return randomString.ToString();
        }
    }
}
