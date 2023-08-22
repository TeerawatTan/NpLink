using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EndoscopicSystem.Helpers
{
    public static class ImageHelper
    {
        public static async Task<Bitmap> CorrectedImage(string originalPathImg)
        {
            // Load the original image
            Image originalImage = Image.FromFile(originalPathImg);

            // Create a new Bitmap with the same size as the original image
            Bitmap correctedImage = new Bitmap(originalImage.Width, originalImage.Height);

            // Create a ColorMatrix that will correct the color of the image
            ColorMatrix colorMatrix = new ColorMatrix(new float[][] {
                new float[] { 1, 0, 0, 0, 0 }, // Red
                new float[] { 0, 1, 0, 0, 0 }, // Green
                new float[] { 0, 0, 1, 0, 0 }, // Blue
                new float[] { 0, 0, 0, 1, 0 }, // Alpha
                new float[] { 0, 0, 0, 0, 1 } // Offset
            });

            // Create an ImageAttributes object with the ColorMatrix
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix);

            // Draw the corrected image onto the Bitmap using the ImageAttributes
            using (Graphics graphics = Graphics.FromImage(correctedImage))
            {
                graphics.DrawImage(originalImage, new Rectangle(0, 0, originalImage.Width, originalImage.Height), 0, 0, originalImage.Width, originalImage.Height, GraphicsUnit.Pixel, imageAttributes);
            }

            return correctedImage;
        }

        public static string GetUntilOrEmpty(string text, string stopAt)
        {
            if (!String.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    return text.Substring(0, charLocation);
                }
            }

            return String.Empty;
        }

        public static byte[] ToByteArray(this Bitmap bitmap)
        {
            // Lock the bitmap's bits
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);

            // Get the address of the first line
            IntPtr ptr = bmpData.Scan0;

            // Calculate the total number of bytes
            int bytes = Math.Abs(bmpData.Stride) * bitmap.Height;

            // Create a byte array to hold the bitmap data
            byte[] rgbValues = new byte[bytes];

            // Copy the bitmap data to the byte array
            Marshal.Copy(ptr, rgbValues, 0, bytes);

            // Unlock the bitmap's bits
            bitmap.UnlockBits(bmpData);

            return rgbValues;
        }
    }
}
