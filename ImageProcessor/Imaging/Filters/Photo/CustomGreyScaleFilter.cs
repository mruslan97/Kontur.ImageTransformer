using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessor.Imaging.Helpers;

namespace ImageProcessor.Imaging.Filters.Photo
{
    internal class CustomGreyScaleFilter : IFilter
    {
        public Bitmap TransformImage(Image source, Image destination)
        {
            unsafe
            {
                var bitMap = new Bitmap(source);
                BitmapData bitmapData = bitMap.LockBits(new Rectangle(0, 0, bitMap.Width, bitMap.Height), ImageLockMode.ReadWrite, bitMap.PixelFormat);
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitMap.PixelFormat) / 8;
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                Parallel.For(0, heightInPixels, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        var newPixel = PixelOperations.GreyScale(Color.FromArgb(currentLine[x],
                            currentLine[x + 1], currentLine[x + 2]));
                        currentLine[x] = newPixel.R;
                        currentLine[x + 1] = newPixel.G;
                        currentLine[x + 2] = newPixel.B;
                    }
                });
                bitMap.UnlockBits(bitmapData);
                return bitMap;
            }
        }
    }
}
