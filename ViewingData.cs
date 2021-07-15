using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBox
{
    public class ViewingData : Data
    {
        private ulong longestSegIndex;
        private ulong[] segsLineView;

        public ViewingData(Bits[] segments)
        : base(segments)
        {
            segsLineView = new ulong[segments.Length+1];
            FindLongestIndex();
        }

        public ViewingData(Data data)
        : base(data)
        {
            segsLineView = new ulong[segments.Length+1];
            FindLongestIndex();
        }

        private void FindLongestIndex()
        {
            if (segments.Length == 0)
                throw new IndexOutOfRangeException();
            ulong index = 0;
            ulong length = 0;
            for (ulong i = 0; i < (ulong)segments.Length; i++)
            {
                if (segments[i].Length > length)
                {
                    length = segments[i].Length;
                    index = i;
                }
            }
            longestSegIndex = index;
        }

        public void UpdateSegsLineView(ulong width)
        {
            ulong lineIndex = 0;
            for(ulong i = 0; i< (ulong)segments.Length; i++)
            {
                segsLineView[i] = lineIndex;
                lineIndex += (ulong)Math.Ceiling((double)segments[i].Length / width);
            }
            segsLineView[segments.Length] = segsLineView[segments.Length - 1] + (ulong)Math.Ceiling((double)segments[segments.Length - 1].Length / width);
        }

        public ulong NumOfLines
        {
            get
            {
                return segsLineView[segsLineView.Length-1];
            }
        }
        
        public ulong findSegAtLine(ulong lineIndex, ulong startSearchLineOffset)
        {
            ulong searchIndex = startSearchLineOffset;
            //ulong jumpConst = ;
            while(true)
            {
                if (segsLineView[searchIndex] <= lineIndex && segsLineView[searchIndex + 1] > lineIndex)
                    return searchIndex;
                //    long temp = (long)searchIndex - (long)segsLineView[searchIndex] + (long)lineIndex;
                //    if (temp < 0)
                //        temp = 0;
                //    if ((ulong)temp > lineIndex)
                //        temp = (long)lineIndex;
                //    searchIndex = (ulong)temp;
                searchIndex++;
            }

        }

        public ulong findBitOffsetAtLine(ulong lineIndex, ulong width)
        {
           return lineIndex * width;
        }
        
        public ulong Longest
        {
            get
            {
                return segments[longestSegIndex].Length;
            }
        }
        public ulong LongestIndex
        {
            get
            {
                return longestSegIndex;
            }
        }
    }
}
