using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBox
{
    
    public class Bits
    {

        byte[] data;
        byte bitsInLastByte;

        public Bits()
        {
            data = new byte[0];
        }
        public Bits(Bits source, ulong byteoffset, ulong bytelength)
        {
            data = new byte[bytelength];
            Array.Copy(source.data, (int)byteoffset, data, 0, (int)bytelength);
            bitsInLastByte = 8;
        }

        public Bits(byte[] data)
        {
            this.data = data;
            bitsInLastByte = 8;
        }

        public void SetBytes(byte[] data)
        {
            this.data = data;
        }

        public override string ToString()
        {
            string result = "0b";
            for(ulong i = 0; i < this.Length; i++)
            {
                if (this[i])
                    result += "1";
                else
                    result += "0";
            }
            return result;
        }



        public bool this[ulong i]
        {
            get
            {
                return (data[i / 8] & (1 << (byte)(i % 8))) != 0;
            }
            set
            {
                if (value)
                    data[i / 8] = (byte)(data[i / 8] & ~(1 << (byte)(i % 8)) | (1 << (byte)(i % 8)));
                else
                    data[i / 8] = (byte)(data[i / 8] & ~(1 << (byte)(i % 8)));
            }
        }

        public ulong Length
        {
            get
            {
                return (ulong)data.Length * 8 - 8 + bitsInLastByte;
            }
        }

        // can be implemented better
        public Bits this[ulong i, ulong j]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {

            }
        }

    }
}
