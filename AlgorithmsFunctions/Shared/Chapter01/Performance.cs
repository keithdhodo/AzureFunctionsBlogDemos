using System;

namespace AlgorithmsFunctions.Shared.Chapter01
{
    public class Performance
    {
        public TimeSpan Runtime { get; set; }
        public string AlgorithmName { get; set; }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
    }
}
