using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBox
{
    public class Data
    {
        protected Bits[] segments;

        public Data(Data data)
        {
            segments = data.segments;
        }

        public Data(Bits[] segments)
        {
            this.segments = segments;
        }

        public Data(List<Bits> segments)
        {
            this.segments = segments.ToArray();
        }

        public ulong Length
        {
            get
            {
                return (ulong)segments.Length;
            }
        }       

        public Bits this[ulong i]
        {
            get 
            {
                if (i > Length)
                    throw new IndexOutOfRangeException();
                return segments[i];
            }
            set 
            {
                if (i > Length)
                    throw new IndexOutOfRangeException();
                segments[i] = value;
            }
        }


    }
}
