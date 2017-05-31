using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureFunctionsBlogDemos.Merging
{
    public class MergePerformance
    {
        public string MergeOutput { get; set; }
        public TimeSpan Runtime { get; set; }

        public string AlgorithmName { get; set; }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
    }
}