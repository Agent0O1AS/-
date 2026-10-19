using System;

namespace SMD_2_3_IS_PR1
{
    class Income : IOperation
    {
        public string type { get; }
        public DateTime date { get; }
        public string source { get; }
        public int sum { get; }

        public Income(string type, DateTime date, string source, int sum)
        {
            this.type = type;
            this.date = date;
            this.source = source;
            this.sum = sum;
        }
    }
}