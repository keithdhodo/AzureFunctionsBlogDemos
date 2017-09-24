using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Diagnostics;
using AlgorithmsFunctions.Shared.Chapter03;
using System.Numerics;
using Newtonsoft.Json;

namespace AlgorithmsFunctions.Chapter03
{
    public static class CoinFlipHttpTrigger
    {
        [FunctionName("CoinFlipHttpTrigger")]
        public static async System.Threading.Tasks.Task<HttpResponseMessage> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "api/chapter03/coinflip")]
            HttpRequestMessage req,
            TraceWriter log)
        {
            log.Info("CoinFlipHttpTrigger function processed a request.");

            // Parse request input
            dynamic data = await req.Content.ReadAsAsync<object>();
            // Set name to body data
            BigInteger input = data?.input;

            if (input < 0)
            {
                return req.CreateResponse(HttpStatusCode.OK, new
                {
                    message = "Cannot flip a coin a negative amount of times. Please enter a value of 0 or greater and retry the request."
                });
            }

            log.Info($"Flipping a coin {input} times. This could take some time...");

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            stopwatch.Stop();

            var coinFlips = new CoinFlip().SimulateCoinFlips(input);

            return req.CreateResponse(HttpStatusCode.OK, new
            {
                message = JsonConvert.SerializeObject(coinFlips)
            });
        }
    }
}