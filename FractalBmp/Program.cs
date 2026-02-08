using System;
using System.IO;

namespace bmp
{
    class Program
    {
        public static int GetPixelIndex(int x, int y, int width, int height)
        {
            int bytesPerPixel = 3;  // RGB = 3 bytes
            int rowSize = width * bytesPerPixel;

            // Calculate padding (rows must be multiple of 4)
            int padding = (4 - (rowSize % 4)) % 4;
            int rowSizeWithPadding = rowSize + padding;

            // BMP stores bottom-to-top, so flip Y coordinate
            int flippedY = height - 1 - y;

            // Calculate index
            int index = (flippedY * rowSizeWithPadding) + (x * bytesPerPixel);

            return index;
        }
        static void Main(string[] args)
        {
            using (FileStream file = new FileStream("sample.bmp", FileMode.Create, FileAccess.Write))
            {
                //Sudaromas bmp formato monochrominis 1000x1000 taškų paveikslėlis
                file.Write(
                new byte[54]
                {
                    //Antraštė
                    0x42, 0x4d,
                    0x3d, 0x0, 0x0, 0x0,
                    0x0, 0x0, 0x0, 0x0,
                    0x36, 0x0, 0x0, 0x0,
                    //Antraštės informacija
                    0x28, 0x0, 0x0, 0x0,
                    0x5, 0x0, 0x0, 0x0,
                    0x5, 0x0, 0x0, 0x0,
                    0x1, 0x0,
                    0x18, 0x0,
                    0x0, 0x0, 0x0, 0x0,
                    0x50, 0x0, 0x0, 0x0,
                    0x0, 0x0, 0x0, 0x0,
                    0x0, 0x0, 0x0, 0x0,
                    0x0, 0x0, 0x0, 0x0,
                    0x0, 0x0, 0x0, 0x0,



                });

                byte[] t = new byte[80];

                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        t[i * 16 + j * 3] = 0xFF;
                        t[i * 16 + j * 3 + 1] = 0xFF;
                        t[i * 16 + j * 3 + 2] = 0xFF;
                    }
                }

                int index = GetPixelIndex(1, 2, 5, 5);
                t[index] = 0x00;
                t[index + 1] = 0x00;
                t[index + 2] = 0x00;
                file.Write(t);

                file.Close();
            }
        }
    }
}
