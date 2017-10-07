using AlgorithmsFunctions.Shared;
using AlgorithmsFunctions.Shared.Chapter03;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AlgorithmsFunctions.Chapter03
{
    public static class CountStringMatchesHttpTrigger
    {
        [FunctionName("CountStringMatchesHttpTrigger")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "api/chapter03/countstringmatches")]
            HttpRequestMessage req, 
            TraceWriter log,
            [Table(Constants.Chapter03.AzureTableStorageChapter03CountStringMatchesTableName, Connection = Constants.AzureWebJobsStorageConnectionString)] IAsyncCollector<CountMatchesPerformance> outputTable)
        {
            log.Info("CountStringMatchesHttpTrigger function processed a request.");

            // Parse request input
            dynamic data = await req.Content.ReadAsAsync<object>();
            // Set name to body data
            string pattern = data?.Pattern;
            string stringToSearch = data?.StringToSearch;

            if (pattern.Length < 1)
            {
                return req.CreateResponse(HttpStatusCode.OK, new
                {
                    message = "The pattern to match must be at least one character long. Please ensure the pattern is longer than zero and retry the request."
                });
            }

            if (stringToSearch.Length < 1)
            {
                return req.CreateResponse(HttpStatusCode.OK, new
                {
                    message = "The string to search must be at least one character long. Please ensure the string to search is longer than zero and retry the request."
                });
            }

            log.Info($"Count the number of times {pattern} appears in {stringToSearch}");

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            stopwatch.Stop();

            var matches = new StringSearch().CountMatches(pattern, stringToSearch);

            var performance = new CountMatchesPerformance();

            performance.Runtime = stopwatch.Elapsed;
            performance.AlgorithmName = Constants.Chapter03.Chapter03CountStringMatches;
            performance.PartitionKey = Constants.Chapter03.Chapter03CountStringMatches;
            performance.RowKey = Guid.NewGuid().ToString();
            performance.Pattern = pattern;
            performance.StringToSearch = stringToSearch;
            performance.Matches = matches;

            outputTable.AddAsync(performance);

            return req.CreateResponse(HttpStatusCode.OK, new
            {
                message = JsonConvert.SerializeObject(matches)
            });
        }
    }
}
