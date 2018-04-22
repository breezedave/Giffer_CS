using System;
using System.Drawing.Imaging;
using Accord.Video.FFMPEG;
using System.Collections.Generic;
using System.Drawing;

namespace Giffer
{
    class VideoReader
    {
        public string videoPath { get; set; }
        public VideoFileReader vr { get; set; }
        public int frameRate { get; set; }
        public string name { get; set; }

        public VideoReader(string path)
        {
            vr = new VideoFileReader();

            videoPath = path;
            vr.Open(path);
            frameRate = (int)vr.FrameRate.ToDouble();
            this.name = path.Split('/')[1].Split('.')[0];
        }

        public List<Image> GetFrames(int sTime, int eTime, string txt)
        {
            var images = new List<Image>();
            var sFrameNum = Convert.ToInt32((vr.FrameRate * sTime).Value);
            var eFrameNum = Convert.ToInt32((vr.FrameRate * eTime).Value);
            frameRate = Convert.ToInt32(vr.FrameRate.ToDouble());

            for (var i = sFrameNum; i < eFrameNum; i++)
            {
                Console.SetCursorPosition(0, 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, 1);
                Console.Write("Extracting Frame " + (i - sFrameNum) + " of " + (eFrameNum - sFrameNum));
                var frame = vr.ReadVideoFrame(i);
                AddText(frame, txt);
                images.Add(frame);
            }
            return images;
        }

        public void AddText(Bitmap frame, string txt)
        {
            using (var myFont = new Font("FoundryPlekW01-Regular", 26f))
            {
                Graphics canvas = Graphics.FromImage(frame);

                var brush = new SolidBrush(Color.FromArgb(200, Color.White)); 
                var rect = new Rectangle(0, vr.Height - vr.Height / 5, vr.Width, vr.Height / 5);
                
                canvas.FillRectangle(brush, rect);

                var stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;

                canvas.DrawString(txt, myFont, Brushes.Black, rect, stringFormat);
            }
        }
    }
}
