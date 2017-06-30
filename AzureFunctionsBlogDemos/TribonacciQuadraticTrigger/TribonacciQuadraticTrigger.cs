using AzureFunctionsBlogDemos.Shared;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionsBlogDemos.Tribonacci
{
    public class TribonacciQuadraticTrigger
    {
        public static void Run(Tribonnaci myQueueItem, TraceWriter log, IAsyncCollector<TribonacciPerformance> outputTable,
            IBinder binder)
        {
            log.Info("TribonacciQuadraticTrigger processed a request.");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var outputArray = myQueueItem.Tribonacci(myQueueItem, Enums.TribonacciAlgorithms.quadratic);

            stopwatch.Stop();

            var performance = new TribonacciPerformance();
            performance.Runtime = stopwatch.Elapsed;
            performance.AlgorithmName = "TribonacciQuadratic";
            performance.PartitionKey = "TribonacciQuadratic";
            performance.RowKey = Guid.NewGuid().ToString();
            performance.Iterations = myQueueItem.Iterations;
            performance.Result = outputArray[myQueueItem.Iterations - 1];

            outputTable.AddAsync(performance);

            var blobPath = "tribonacci" + "/" + "tribonacciquadratic" + DateTime.UtcNow.ToString("yyyyMMddHHmmss") + ".txt";

            using (var outputBlob = binder.Bind<TextWriter>(
                new BlobAttribute(blobPath)))
            {
                outputBlob.WriteLine($"Signature array: {string.Join(",", myQueueItem.StartingSet)}");
                outputBlob.WriteLine($"Iterations: {string.Join(",", myQueueItem.Iterations)}");
                outputBlob.WriteLine($"Number at {myQueueItem.Iterations}: {outputArray[myQueueItem.Iterations - 1]}");
                outputBlob.WriteLine($"Output Array: {string.Join(",", outputArray)}");
                outputBlob.WriteLine();
                outputBlob.WriteLine($"Runtime: {performance.Runtime.ToString()}");

                // create Sha512 Hash
                var sha = new SHA512CryptoServiceProvider();
                // This is one implementation of the abstract class SHA512.
                var result = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(blobPath + myQueueItem.StartingSet + myQueueItem.Iterations + outputArray[myQueueItem.Iterations - 1] + outputArray + performance.Runtime));
                outputBlob.WriteLine($"Hash of signature array, iterations, number at iteration, output array and Runtime: {BitConverter.ToString(result).Replace("-", "")}");
            }

            log.Info($"Signature array: {string.Join(",", myQueueItem.StartingSet)}");
            log.Info($"Iterations: {string.Join(",", myQueueItem.Iterations)}");
            log.Info($"Number at {myQueueItem.Iterations}: {outputArray[myQueueItem.Iterations - 1]}");
            log.Info($"Output Array: {string.Join(",", outputArray)}");
            log.Info($"Runtime: {performance.Runtime.ToString()}");
        }
    }
}
