﻿using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Diagnostics;

namespace AzureFunctionsBlogDemos.Merging
{
    public class QuickUnionTopicTrigger
    {
        public static void Run(Array myQueueItem, TraceWriter log, IAsyncCollector<Array> outputTable)
        {
            log.Info("QuickUnionTopicTrigger processed a request.");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Array.QuickUnion(myQueueItem);

            stopwatch.Stop();
            myQueueItem.Runtime = stopwatch.Elapsed;

            myQueueItem.AlgorithmName = "QuickUnion";
            myQueueItem.PartitionKey = "QuickUnion";
            myQueueItem.RowKey = Guid.NewGuid().ToString();

            outputTable.AddAsync(myQueueItem);
        }
    }
}