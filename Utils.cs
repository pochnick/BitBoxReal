using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBox
{
    public class Utils
    {
        /// <summary>
        /// get file data to bits Data object
        /// </summary>
        /// <param name="sdress"></param>
        /// <returns></returns>
        public static Data OpenFile(string inputFilename)
        {
            return new Data(new Bits[] {new Bits(File.ReadAllBytes(inputFilename))});
        }
    }
}
