﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net_task_2.Model
{
   public  class ImageConvert
    {
        public static byte[] GetBytesOfImage(string path)
        {
            var image = new Bitmap(path);
            ImageConverter imageconverter = new ImageConverter();

            var imagebytes = ((byte[])imageconverter.ConvertTo(image, typeof(byte[])));
            return imagebytes;
        }
        public static string GetImagePath(byte[] buffer, int counter)
        {
            ImageConverter ic = new ImageConverter();
            Image img = (Image)ic.ConvertFrom(buffer);
            Bitmap bitmap1 = new Bitmap(img);
            bitmap1.Save($@"C:\Users\admin\Pictures\Saved Pictures\image{counter}.png");
            var imagepath = $@"C:\Users\admin\Pictures\Saved Pictures\image{counter}.png";
            return imagepath;
        }
    }
}
