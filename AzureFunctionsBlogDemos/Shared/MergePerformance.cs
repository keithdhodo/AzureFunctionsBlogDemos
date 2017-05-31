using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureFunctionsBlogDemos.Merging
{
    public class MergePerformance
    {
        public TimeSpan Runtime { get; set; }

        public string AlgorithmName { get; set; }

        public int NumberOfElements { get; set; }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
    }
}