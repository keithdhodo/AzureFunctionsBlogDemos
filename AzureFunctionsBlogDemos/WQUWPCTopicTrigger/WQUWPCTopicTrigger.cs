using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Diagnostics;

namespace AzureFunctionsBlogDemos.Merging
{
    public class WQUWPCTopicTrigger
    {

        public static void Run(Array myQueueItem, TraceWriter log, IAsyncCollector<Array> outputTable)
        {
            log.Info("WQUWPCTopicTrigger processed a request.");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Array.WeightedQuickUnionWithPathCompression(myQueueItem);

            stopwatch.Stop();
            myQueueItem.Runtime = stopwatch.Elapsed;

            myQueueItem.AlgorithmName = "WeightedQuickUnionWithPathCompression";
            myQueueItem.PartitionKey = "WeightedQuickUnionWithPathCompression";
            myQueueItem.RowKey = Guid.NewGuid().ToString();

            outputTable.AddAsync(myQueueItem);
        }
    }
}