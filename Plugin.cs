using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBox
{
    public abstract class Plugin
    {
        /// <summary>
        /// parse data and return another data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public abstract Data Parse(Data data);
        protected string name;
        protected string description;
        
        public string Name
        {
            get
            {
                return name;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
        }
    }
}
