using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;

namespace NamadBourse
{
    class Program
    {
        static string address = "http://www.tsetmc.com/tsev2/data/MarketWatchPlus.aspx";
        static string output = "namad.txt";
        static void Main(string[] args)
        {
            using (var cl = new WebClient())
            {
                var raw = new StreamReader(new GZipStream(cl.OpenRead(address), CompressionMode.Decompress)).ReadToEnd();

                string[] labels = raw.Split(';').Skip(1).Select(x => x.Split(',')).ToArray().Select(x => x[2]).Where(x => (int.TryParse(x, out _) ? false : true)).ToArray();

                File.WriteAllLines(output, labels, Encoding.Unicode);
            }
        }
    }
}
