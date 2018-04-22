using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Giffer
{
    class ReadSrt
    {
        public string file { get; set; }
        public string name { get; set; }
        public List<GifDetails> chunks { get; set; }

        public ReadSrt(string path)
        {
            name = path;
            file = System.IO.File.ReadAllText(path);
            chunks = new List<GifDetails>();
        }

        public void GetChunks()
        {
            var lines = file.Split('\n');
            var startRegex = new Regex("-->", RegexOptions.IgnoreCase);
            string sTime = "";
            string eTime = "";
            string str = "";

            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if(startRegex.Match(line).Success)
                {
                    sTime = line.Substring(0 ,12);
                    eTime = line.Substring(17, 12);
                    continue;
                }
                int parsedLine = -1;
                int.TryParse(line, out parsedLine);
                if (parsedLine > 0)
                {
                    var chunk = new GifDetails()
                    {
                        Source = name,
                        Txt = str.Trim().Replace("<i>","").Replace("</i>",""),
                        StartTime = sTime,
                        EndTime = eTime
                    };
                    chunks.Add(chunk);
                    str = string.Empty;
                    continue;
                }
                str += line + '\n';
            }


        }

    }
}
