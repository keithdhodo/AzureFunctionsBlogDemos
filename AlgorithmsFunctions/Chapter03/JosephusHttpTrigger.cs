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
    public class JosephusHttpTrigger
    {
        [FunctionName("JosephusHttpTrigger")]
        public static async System.Threading.Tasks.Task<HttpResponseMessage> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "api/chapter03/josephus")]
            HttpRequestMessage req,
            TraceWriter log)
        {
            log.Info("JosephusHttpTrigger function processed a request.");

            // Parse request input
            dynamic data = await req.Content.ReadAsAsync<object>();
            // Set name to body data
            int numberOfParticipants = data?.NumberOfParticipants;
            int orderToEliminate = data?.OrderToEliminate;

            if (numberOfParticipants <= 0)
            {
                return req.CreateResponse(HttpStatusCode.OK, new
                {
                    message = "The number of participants needs to be greater than zero. Please enter a value greater than zero and retry the request."
                });
            }

            log.Info($"Creating Josephus problem with {numberOfParticipants} participants.");
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var finalNode = new Josephus(numberOfParticipants, orderToEliminate).ExecuteJosephusSimulation();

            stopwatch.Stop();

            return req.CreateResponse(HttpStatusCode.OK, new
            {
                message = JsonConvert.SerializeObject($"The winner is: {finalNode.Item}")
            });
        }
    }
}
