using AlgorithmsFunctions.Shared;
using AlgorithmsFunctions.Shared.Chapter01;
using AlgorithmsFunctions.Shared.Chapter01.Enums;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;

namespace AlgorithmsFunctions.Chapter01
{
    public static class QuickUnionTopicTrigger
    {
        [FunctionName("QuickUnionTopicTrigger")]
        public static void Run(
            [ServiceBusTrigger(Constants.Chapter01.AzureServiceBusTopicNameChapter01, 
                Constants.Chapter01.Chapter01QuickUnion, 
                AccessRights.Manage, 
                Connection = Constants.AzureServiceBusConnectionString)]
            MergingArray myQueueItem,
            TraceWriter log,
            [Table(Constants.Chapter01.AzureTableStorageChapter01, Constants.AzureWebJobsStorageConnectionString)] IAsyncCollector<MergePerformance> outputTable,
            IBinder binder)
        {
            log.Info("QuickUnionTopicTrigger processed a request.");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            myQueueItem.Merge(myQueueItem, MergeAlgorithms.QuickUnion);

            stopwatch.Stop();

            var performance = new MergePerformance();

            performance.Runtime = stopwatch.Elapsed;
            performance.AlgorithmName = Constants.Chapter01.Chapter01QuickUnion;
            performance.PartitionKey = Constants.Chapter01.Chapter01QuickUnion;
            performance.RowKey = Guid.NewGuid().ToString();
            performance.NumberOfElements = myQueueItem.Output.Length;

            outputTable.AddAsync(performance);

            var blobPath = "merging" + "/" + "quickunion" + DateTime.UtcNow.ToString("yyyyMMddHHmmss") + ".txt";

            var attributes = new Attribute[]
            {
                new BlobAttribute(blobPath),
                new StorageAccountAttribute(Constants.AzureWebJobsStorageConnectionString)
            };

            using (var outputBlob = binder.Bind<TextWriter>(
                new BlobAttribute(blobPath)))
            {
                outputBlob.WriteLine($"Number to Union From: {string.Join(",", myQueueItem.NumberToUnionFrom)}");
                outputBlob.WriteLine($"Number to Union To: {string.Join(",", myQueueItem.NumberToUnionTo)}");
                outputBlob.WriteLine($"Output of Merge: {string.Join(",", myQueueItem.Output)}");
                outputBlob.WriteLine();
                outputBlob.WriteLine($"Runtime: {performance.Runtime.ToString()}");

                // create Sha512 Hash
                var sha = new SHA512CryptoServiceProvider();
                // This is one implementation of the abstract class SHA512.
                var result = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(blobPath + myQueueItem.NumberToUnionFrom + myQueueItem.NumberToUnionTo + myQueueItem.Output));
                outputBlob.WriteLine($"Hash of Inputs, Output and Runtime: {BitConverter.ToString(result).Replace("-", "")}");
            }
        }
    }
}