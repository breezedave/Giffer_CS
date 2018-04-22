using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giffer
{
    class Program
    {
        static void Main(string[] args)
        {
            var inFileVideo = "D:/S01E02.avi";
            var inFileSrt = "D:/S01E02.srt";
            var folder = inFileVideo.Split('/')[1].Split('.')[0];
            var srtReader = new ReadSrt(inFileSrt);
            srtReader.GetChunks();

            var vr = new VideoReader(inFileVideo);
            var delay = Convert.ToInt16(100 / vr.frameRate);
            System.IO.Directory.CreateDirectory("D:/" + folder);

            for(var i = 1; i < srtReader.chunks.Count; i++)
            {
                var chunk = srtReader.chunks[i];
                var sTime = GetTime(chunk.StartTime);
                var eTime = GetTime(chunk.EndTime);
                var images = vr.GetFrames(sTime, eTime, chunk.Txt);
                var gif = new GifCreate();

                Console.SetCursorPosition(0, 0);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, 0);
                Console.Write("Gif " + i + " of " + srtReader.chunks.Count);

                gif.Create(images, delay, "D:/" + folder + "/", chunk.Txt);

            }
        }

        static int GetTime(string time)
        {
            var h = Convert.ToInt32(time.Substring(0, 2));
            var m = Convert.ToInt32(time.Substring(3, 2));
            var s = Convert.ToInt32(time.Substring(6, 2));
            var ms = Convert.ToInt32(time.Substring(9, 3));

            return Convert.ToInt32(Math.Round((Double)(h * 3600) + (m * 60) + s + (ms / 1000))); 
        }
    }
}
