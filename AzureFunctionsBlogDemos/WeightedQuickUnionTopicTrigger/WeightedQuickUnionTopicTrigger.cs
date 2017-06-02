using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;

namespace AzureFunctionsBlogDemos.Merging
{
    public class WeightedQuickUnionTopicTrigger
    {

        public static void Run(MergingArray myQueueItem, TraceWriter log, IAsyncCollector<MergePerformance> outputTable,
            IBinder binder)
        {
            log.Info("WeightedQuickUnionTopicTrigger processed a request.");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            MergingArray.Merge(myQueueItem, Shared.Enums.MergeAlgorithms.WeightedQuickUnionWithPathCompression);

            stopwatch.Stop();

            var performance = new MergePerformance();
            performance.Runtime = stopwatch.Elapsed;
            performance.AlgorithmName = "WeightedQuickUnion";
            performance.PartitionKey = "WeightedQuickUnion";
            performance.RowKey = Guid.NewGuid().ToString();
            performance.NumberOfElements = myQueueItem.Output.Length;

            outputTable.AddAsync(performance);

            var blobPath = "merging" + "/" + "weightedquickuniontopictrigger" + DateTime.UtcNow.ToString("yyyyMMddHHmmss") + ".txt";

            using (var outputBlob = binder.Bind<TextWriter>(
                new BlobAttribute(blobPath)))
            {
                outputBlob.WriteLine($"Number to Union From: {string.Join(",", myQueueItem.NumberToUnionFrom)}");
                outputBlob.WriteLine($"Number to Union To: {string.Join(",", myQueueItem.NumberToUnionTo)}");
                outputBlob.WriteLine($"Output of Merge: {string.Join(",", myQueueItem.Output)}");
                outputBlob.WriteLine();
                outputBlob.WriteLine($"Runtime: {performance.Runtime.ToString()}");

                // create Sha1 Hash
                var sha = new SHA512CryptoServiceProvider();
                // This is one implementation of the abstract class SHA512.
                var result = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(blobPath + myQueueItem.NumberToUnionFrom + myQueueItem.NumberToUnionTo + myQueueItem.Output));
                outputBlob.WriteLine($"Hash of Inputs, Output and Runtime: {BitConverter.ToString(result).Replace("-", "")}");
            }
        }
    }
}