// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CropLayer.cs" company="James Jackson-South">
//   Copyright (c) James Jackson-South.
//   Licensed under the Apache License, Version 2.0.
// </copyright>
// <summary>
//   Encapsulates the properties required to crop an image.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Security.Cryptography.X509Certificates;
using ImageProcessor.Common.Exceptions;

namespace ImageProcessor.Imaging
{
    using System;

    /// <summary>
    /// Encapsulates the properties required to crop an image.
    /// </summary>
    public class CropLayer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CropLayer"/> class.
        /// </summary>
        /// <param name="left">The left coordinate of the crop layer.</param>
        /// <param name="top">The top coordinate of the crop layer.</param>
        /// <param name="right">The right coordinate of the crop layer.</param>
        /// <param name="bottom">The bottom coordinate of the crop layer.</param>
        /// <param name="cropMode">The <see cref="CropMode"/>.</param>
        /// <remarks>
        /// If the <see cref="CropMode"/> is set to <value>CropMode.Percentage</value> then the four coordinates
        /// become percentages to reduce from each edge.
        /// </remarks>
        public CropLayer(int x, int y, int width, int height)
        {
            if (x < 1 && width < 1 || y < 1 && height < 1)
                throw new ImageProcessingException("Invalid coordinates");

            if (x < 0)
            {
                width = Math.Abs(x + width);
                x = 0;
            }

            if (y < 0)
            {
                height = Math.Abs(y + height);
                y = 0;
            }

            if (width < 0)
            {
                width = x;
            }

            if (height < 0)
            {
                height = y;
            }
            //if (left < 0 || top < 0 || right < 0 || bottom < 0)
            //{
            //    throw new ArgumentOutOfRangeException();
            //}

            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// Gets or sets the left coordinate of the crop layer.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the top coordinate of the crop layer.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets the right coordinate of the crop layer.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the bottom coordinate of the crop layer.
        /// </summary>
        public int Height { get; set; }
        
    }
}
