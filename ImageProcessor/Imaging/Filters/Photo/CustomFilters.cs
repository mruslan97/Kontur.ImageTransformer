using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessor.Imaging.Filters.Photo
{
    public static class CustomFilters
    {
        public static IFilter CustomGreyScaleFilter => new CustomGreyScaleFilter();

        public static IFilter CustomSepiaFilter => new CustomSepiaFilter();
    }
}
