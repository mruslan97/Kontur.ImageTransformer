using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessor.Common.Exceptions;
using ImageProcessor.Imaging.Helpers;

namespace ImageProcessor.Processors
{
    public class CustomFilter : IGraphicsProcessor
    {
        public dynamic DynamicParameter
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets any additional settings required by the processor.
        /// </summary>
        public Dictionary<string, string> Settings
        {
            get;
            set;
        }

        public Image ProcessImage(ImageFactory factory)
        {
            Bitmap newImage = null;
            Image image = factory.Image;
            int parameter = DynamicParameter;
            if(parameter>100 || parameter<0)
                throw new ArgumentOutOfRangeException();
            var bitMap = new Bitmap(image);
            try
            {
                using (Graphics graphics = Graphics.FromImage(image))
                {
                    using (ImageAttributes attributes = new ImageAttributes())
                    {
                        unsafe
                        {
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
                                    var newPixel = PixelOperations.Threshold(Color.FromArgb(currentLine[x],
                                        currentLine[x + 1], currentLine[x + 2]),parameter);
                                    currentLine[x] = newPixel.R;
                                    currentLine[x + 1] = newPixel.G;
                                    currentLine[x + 2] = newPixel.B;
                                }
                            });
                            bitMap.UnlockBits(bitmapData);
                            image = bitMap;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                newImage?.Dispose();

                throw new ImageProcessingException("Error processing image with " + this.GetType().Name, ex);
            }

            return image;
        }

    }
}
