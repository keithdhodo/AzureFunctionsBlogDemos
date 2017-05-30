using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Diagnostics;

namespace AzureFunctionsBlogDemos.Merging
{
    public class QuickFindSBTopicTrigger
    {
        public static void Run(Array myQueueItem, TraceWriter log, IAsyncCollector<Array> outputTable)
        {
            log.Info("QuickFindSBTopicTrigger processed a request.");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Array.QuickFind(myQueueItem);

            stopwatch.Stop();
            myQueueItem.Runtime = stopwatch.Elapsed;

            myQueueItem.AlgorithmName = "QuickFind";
            myQueueItem.PartitionKey = "QuickFind";
            myQueueItem.RowKey = Guid.NewGuid().ToString();

            outputTable.AddAsync(myQueueItem);
        }
    }
}