using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBox.Plugins
{
    class JustExamplePlugin : Plugin
    {
        public JustExamplePlugin()
        {
            this.name = "just plugin";
            this.description = "not important";
        }

        public override Data Parse(Data data)
        {
            List<Bits> newData = new List<Bits>();
            bool[] remember = new bool[]{true, true, true, true, true, true, true, true };
            for (ulong i = 0; i < data.Length; i++)
            {
                ulong offset = 0;
                for(ulong j = 0; j< data[i].Length; j++)
                {
                    if (!data[i][j] && !remember[0] && !remember[1] && !remember[2] && !remember[3] && !remember[4] && !remember[5] && !remember[6] && !remember[7])
                    {
                        newData.Add(new Bits(data[i], offset / 8, (j - offset) / 8+1));
                        offset = j;
                    }
                    remember[7] = remember[6];
                    remember[6] = remember[5];
                    remember[5] = remember[4];
                    remember[4] = remember[3];
                    remember[3] = remember[2];
                    remember[2] = remember[1];
                    remember[1] = remember[0];
                    remember[0] = data[i][j];
                }
            }
            /*for (ulong i = 0; i < data.Length; i++)
            {
                ulong offset = 0;
                for (ulong j = 0; j < data[i].Length; j++)
                {
                    newData.Add(new Bits(data[i][7-j+(ulong)(j/8)]));
                }
            }*/
            


            return new Data(newData);
        }
    }
}
