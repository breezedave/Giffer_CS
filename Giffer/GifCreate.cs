using ImageMagick;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

public class GifCreate
{

    public void Create(List<Image> images, int delay, string folder, string path)
    {
        using (MagickImageCollection collection = new MagickImageCollection())
        {
            for(var i = 0; i < images.Count; i++)
            {
                Console.SetCursorPosition(0, 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, 1);
                Console.Write("Saving Frame " + i + " of " + images.Count);
                var img = new MagickImage((Bitmap)images[i]);
                collection.Add(img);
                collection[i].AnimationDelay = delay;
            }

            path = SanitizePath(path);
            try
            {
                collection.Optimize();
                collection.Write(folder + path + ".gif");
            } catch(Exception err) { }
        }
    }

    public string SanitizePath(string path)
    {
        foreach (var c in Path.GetInvalidPathChars())
            path = path.Replace(c, ' ');

        foreach (var c in Path.GetInvalidFileNameChars())
            path = path.Replace(c, ' ');

        return path;
    }

}

