using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AzureFunctionsBlogDemos.Merging
{
    public class QuickUnion
    {
        public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log,
            IAsyncCollector<Array> outputTable)
        {

            log.Info("QuickUnionTrigger processed a request.");

            // Parse request input
            string jsonContent = await req.Content.ReadAsStringAsync();
            var quickFind = JsonConvert.DeserializeObject<Array>(jsonContent);
            log.Info($"Inputs: {quickFind.NumberToUnionFrom.ToString()}, {quickFind.NumberToUnionTo.ToString()}");

            if (quickFind.NumberToUnionFrom.Length != quickFind.NumberToUnionTo.Length)
            {
                return req.CreateResponse(HttpStatusCode.OK, new
                {
                    message = "Number of items to union do not match."
                });
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Array.QuickUnion(quickFind);

            stopwatch.Stop();
            quickFind.Runtime = stopwatch.Elapsed;

            quickFind.AlgorithmName = "QuickUnion";
            quickFind.PartitionKey = "Sorting";
            quickFind.RowKey = Guid.NewGuid().ToString();

            await outputTable.AddAsync(quickFind);

            return req.CreateResponse(HttpStatusCode.OK, new
            {
                message = JsonConvert.SerializeObject(quickFind)
            });
        }
    }
}