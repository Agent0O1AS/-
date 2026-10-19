using System;

namespace SMD_2_3_IS_PR1
{
    interface IOperation
    {
        string type { get; }
        DateTime date { get; }
        string source { get; }
        int sum { get; }
    }
}