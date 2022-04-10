using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDbConnector
{
    public class ScriptHelper
    {
        public void CreateDummyInsertsForSproc()
        {
            var inFile = @"C:\temp\urlCache.csv";
            var outFile = @"C:\temp\urlCacheUpdated.csv"; ;


            //  StreamReader sr = new StreamReader(inFile);
            FileStream writer = File.OpenWrite(outFile);

            int counter = 2;

            //  while (sr.ReadLine() != null)
            string[] lines = File.ReadAllLines(inFile);
            foreach (string sr in lines)
            {
                if (counter == 332)
                {
                    Console.WriteLine("*******************");
                }
                try
                {
                    Console.WriteLine(counter + sr);
                    // writer.Write( " insert into[dbo].[UrlCache]([id],[url]) values(" + counter + ", '" + sr.ReadLine() + "')");

                    var data = (" insert into[dbo].[UrlCache]([id],[url]) values(" + counter + ", '" + sr + "') \n");
                    byte[] bytes = Encoding.UTF8.GetBytes(data);

                    writer.Write(bytes, 0, bytes.Length);
                    writer.Flush();
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
                counter++;
            }

            writer.Close();
            Console.ReadKey();
        }
    }
}
