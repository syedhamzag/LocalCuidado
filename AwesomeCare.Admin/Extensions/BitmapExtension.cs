using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace System.Drawing
{
    public static class BitmapExtension
    {
        public static byte[] ToByteArray(this Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
