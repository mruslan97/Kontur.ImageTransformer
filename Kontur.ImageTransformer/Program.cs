using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace Kontur.ImageTransformer
{
    public class Program
    {
        static void Main(string[] args)
        {
            var baseAddress = "http://+:8080/";
            using (WebApp.Start<Startup>(baseAddress))
            {
                Console.ReadLine();
            }
        }
    }
}
