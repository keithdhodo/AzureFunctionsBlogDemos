using AlgorithmsFunctions.Shared.Chapter03;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Net.Http;

namespace AlgorithmsFunctions.Chapter03
{
    public static class PrimesHttpTrigger
    {
        [FunctionName("PrimesHttpTrigger")]
        public static async System.Threading.Tasks.Task<HttpResponseMessage> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "api/chapter03/sieveoferatosthenes")]
            HttpRequestMessage req,
            TraceWriter log)
        {
            log.Info("PrimesHttpTrigger function processed a request.");

            // Parse request input
            dynamic data = await req.Content.ReadAsAsync<object>();
            // Set name to body data
            int input = data?.input;

            if (input <= 1)
            {
                return req.CreateResponse(HttpStatusCode.OK, new
                {
                    message = "The least Prime number is 2. Please enter a value greater than 2 and retry the request."
                });
            }

            log.Info($"Find all the Prime numbers less than: {input}");

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            stopwatch.Stop();

            var primes = new Primes().FindPrimes(input);

            return req.CreateResponse(HttpStatusCode.OK, new
            {
                message = JsonConvert.SerializeObject(primes)
            });
        }
    }
}
