using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AzureFunctionsBlogDemos.Merging
{
    public class WeightedQuickUnionWithPathCompression
    {

        public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log,
            IAsyncCollector<Array> outputTable)
        {

            log.Info("WeightedQuickUnionWithPathCompressionTrigger processed a request.");

            // Parse request input
            string jsonContent = await req.Content.ReadAsStringAsync();
            var weightedQuickUnionWithPathCompression = JsonConvert.DeserializeObject<Array>(jsonContent);
            log.Info($"Inputs: {weightedQuickUnionWithPathCompression.NumberToUnionFrom.Select(s => s.ToString())}, {weightedQuickUnionWithPathCompression.NumberToUnionTo.Select(s => s.ToString())}");

            if (weightedQuickUnionWithPathCompression.NumberToUnionFrom.Length != weightedQuickUnionWithPathCompression.NumberToUnionTo.Length)
            {
                return req.CreateResponse(HttpStatusCode.OK, new
                {
                    message = "Number of items to union do not match."
                });
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Array.WeightedQuickUnionWithPathCompression(weightedQuickUnionWithPathCompression);

            stopwatch.Stop();
            weightedQuickUnionWithPathCompression.Runtime = stopwatch.Elapsed;

            weightedQuickUnionWithPathCompression.AlgorithmName = "WeightedQuickUnionWithPathCompression";
            weightedQuickUnionWithPathCompression.PartitionKey = "Sorting";
            weightedQuickUnionWithPathCompression.RowKey = Guid.NewGuid().ToString();

            await outputTable.AddAsync(weightedQuickUnionWithPathCompression);

            return req.CreateResponse(HttpStatusCode.OK, new
            {
                message = JsonConvert.SerializeObject(weightedQuickUnionWithPathCompression)
            });
        }
    }
}