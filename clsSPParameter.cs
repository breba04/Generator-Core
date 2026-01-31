using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_Core
{
    public class clsSPParameter
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool isOutput { get; set; }
        public clsSPParameter(string Name, string Type, bool isOutput = false)
        {
            this.Name = Name;
            this.Type = Type;
            this.isOutput = isOutput;
        }
    }
}
