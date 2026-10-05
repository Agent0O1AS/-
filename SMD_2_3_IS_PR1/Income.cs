using System;
using System.Collections.Generic;
using System.Text;

namespace SMD_2_3_IS_PR1
{
    internal class Income
    {
        public string type;
        public DateTime date;
        public string source;
        public int sum;
        public Income(string type, DateTime date, string source, int sum)
        {
            this.type = type;
            this.date = date;
            this.source = source;
            this.sum = sum;
        }
    }
}
