using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace ReDiablo.Data
{
    class GfxLoader
    {
        public GfxLoader()
        {

        }

        public int readFrameCount(byte[] data)
        {
            int count = (int)BitConverter.ToUInt32(data, 0);

            if (count > 1024)
            {
                throw new Exception("Frame count is too high invalid file most likely");
            }

            return count;
        }

        public Bitmap readFrame(byte[] data, int width, int height, Color[] pal, int index)
        {
            int x = 0, y = height-1;
            uint start = BitConverter.ToUInt32(data, 4 + index * 4);
            uint end = BitConverter.ToUInt32(data, 8 + index * 4);
            uint pos = start;
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            uint z;

            while (pos < end && y >= 0)
            {
                byte c = data[pos];

                if (pos == start && c < 16)
                {
                    z = data[pos + 1];

                    if (z == 0)
                    {
                        pos += c;
                        continue;
                    }
                }

                pos++;
                
                if (c <= 127)
                {
                    for (int i = 0; i < c; i++)
                    {
                        byte colorIndex = 0;

                        if (pos < end)
                        {
                            colorIndex = data[pos];
                        }

                        pos++;

                        if (x < width)
                        {
                            bmp.SetPixel(x, y, pal[colorIndex]);
                        }

                        x++;
                        
                    }
                }
                else
                {
                    x += (256 - c);
                }

                if (x >= width)
                {
                    y--;
                    //x = 0;
                    x -= width;
                    if (x < 0) { x = 0; }
                }
            }

            return bmp;
        }

        public Bitmap readFrame2(byte[] data, int width, int height, Color[] pal, int index)
        {
            int x = 0, y = height - 1;
            uint start = BitConverter.ToUInt32(data, 4 + index * 4);
            uint end = BitConverter.ToUInt32(data, 8 + index * 4);
            uint pos = start;
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            uint z;
            ushort[] header = new ushort[5];

            if (data[pos] == 20)
            {
                for (int i = 0; i < 5; i++)
                {
                    header[4-i] = BitConverter.ToUInt16(data, (int)pos + 1 + i*2);
                }

                pos = 21;
            }

            Console.WriteLine(String.Join(",", header));
            
            while (pos < end && y >= 0)
            {
                byte c = data[pos];

                /*
                if (pos == start && c < 16)
                {
                    z = data[pos + 1];

                    if (z == 0)
                    {
                        pos += c;
                        continue;
                    }
                }
                */

                pos++;

                if (c >= 192)
                {
                    c = (byte)(256 - c);

                    for (int i = 0; i < c; i++)
                    {
                        byte colorIndex = 0;

                        if (pos < end)
                        {
                            colorIndex = data[pos];
                        }

                        pos++;

                        if (x < width && y < height && y >= 0)
                        {
                            bmp.SetPixel(x, y, pal[colorIndex]);
                        }

                        x++;

                        if (x >= width)
                        {
                            y--;
                            x = 0;
                        }
                    }

                    //if (x >= width)
                    //{
                        //y--;
                       // x = 0;
                        //x -= width;
                        //if (x < 0) { x = 0; }
                    //}
                }
                else if (c >= 128)
                {
                    c = (byte)(192 - c);

                    if (pos < end)
                    {
                        byte colorIndex = data[pos++];

                        for (int i = 0; i < c; i++)
                        {
                            if (x < width && y < height && y >= 0)
                            {
                                bmp.SetPixel(x, y, pal[colorIndex]);
                            }

                            x++;

                            if (x >= width)
                            {
                                y--;
                                x = 0;
                                //x -= width;
                                //if (x < 0) { x = 0; }
                            }
                        }
                    }
                }
                else
                {
                    x += (128 - c);

                    if (x >= width)
                    {
                        y--;
                        //x = 0;
                        x -= width;
                        if (x < 0) { x = 0; }
                    }
                }


            }

            return bmp;
        }
    }
}
