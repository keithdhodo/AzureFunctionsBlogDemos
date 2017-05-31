using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Diagnostics;

namespace AzureFunctionsBlogDemos.Merging
{
    public class WQUWPCTopicTrigger
    {

        public static void Run(Array myQueueItem, TraceWriter log, IAsyncCollector<MergePerformance> outputTable)
        {
            log.Info("WQUWPCTopicTrigger processed a request.");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Array.Merge(myQueueItem, Shared.Enums.MergeAlgorithms.WeightedQuickUnionWithPathCompression);

            stopwatch.Stop();

            var performance = new MergePerformance();
            performance.Runtime = stopwatch.Elapsed;
            performance.AlgorithmName = "WeightedQuickUnionWithPathCompression";
            performance.PartitionKey = "WeightedQuickUnionWithPathCompression";
            performance.RowKey = Guid.NewGuid().ToString();
            performance.MergeOutput = string.Join(",", myQueueItem.Output);

            outputTable.AddAsync(performance);
        }
    }
}