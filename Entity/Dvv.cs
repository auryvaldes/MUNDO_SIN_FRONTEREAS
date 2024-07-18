using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
   public class Dvv :ICloneable
    {
        public int id { get; set; }
        public String tabla { get; set; }
        public String valor { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
